//
//  ScheduleService.swift
//  IzhTransport
//
//  Created by Nikita Karalius on 22.05.2022.
//

import Foundation

enum ScheduleServiceError: Swift.Error {
    case noResponse, invalidRoute, decoding, session
}

protocol ScheduleServiceProtocol {
    func arrivals(for route: TransportRoute, completion: @escaping ([TransportStop], ScheduleServiceError?) -> Void)
}

final class ScheduleService: ScheduleServiceProtocol {
    func arrivals(
        for route: TransportRoute,
        completion: @escaping ([TransportStop], ScheduleServiceError?) -> Void) {
        let encoder = JSONEncoder()
        guard let encodedRoute = try? encoder.encode(route) else {
            completion([], .invalidRoute)
            return
        }

        let request = postRequest(with: encodedRoute)

        let task = URLSession.shared.dataTask(with: request) { data, _, internalError in
            guard internalError == nil else {
                completion([], .session)
                return
            }
            guard let data = data else {
                completion([], .noResponse)
                return
            }
            let decoder = JSONDecoder()
            let response = try? decoder.decode([TransportStopDTO].self, from: data)
            guard let response = response else {
                completion([], .decoding)
                return
            }
            let model = response.map { $0.model }
            completion(model, .none)
        }

        task.resume()
    }

    private func postRequest(with data: Data) -> URLRequest {
        var request = URLRequest(url: API.scheduleService)
        request.httpMethod = "POST"
        request.setValue("application/json", forHTTPHeaderField: "Content-Type")
        request.httpBody = data
        return request
    }
}
