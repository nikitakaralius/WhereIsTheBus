//
//  ArrivalDto.swift
//  IzhTransport
//
//  Created by Nikita Karalius on 22.05.2022.
//

import Foundation

struct ArrivalDTO: Codable {
    let transportNumber: Int
    let timeToArrive: String
}

extension ArrivalDTO {
    var model: Arrival {
        Arrival(
            transportNumber: self.transportNumber,
            timeToArrive: TimeToArrive(from: self.timeToArrive))
    }
}
