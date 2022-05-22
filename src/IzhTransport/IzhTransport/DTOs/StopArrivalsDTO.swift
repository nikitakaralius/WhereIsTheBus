//
//  StopArrivalsDTO.swift
//  IzhTransport
//
//  Created by Nikita Karalius on 22.05.2022.
//

import Foundation

struct StopArrivalsDTO: Codable {
    let transport: TransportType
    let arrivals: [ArrivalDTO]
}

extension StopArrivalsDTO {
    var model: StopArrivals {
        StopArrivals(transport: self.transport, arrivals: self.arrivals.map { $0.model })
    }
}
