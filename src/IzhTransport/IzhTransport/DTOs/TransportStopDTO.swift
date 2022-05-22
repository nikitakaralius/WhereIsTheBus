//
//  TransportStopDto.swift
//  IzhTransport
//
//  Created by Nikita Karalius on 22.05.2022.
//

import Foundation

struct TransportStopDTO: Codable {
    let id: Int
    let name: String
    let direction: StrictDirection
    let timeToArrive: Int
}

extension TransportStopDTO {
    var model: TransportStop {
        TransportStop(
            id: self.id,
            name: self.name,
            direction: self.direction,
            timeToArrive: .minutes(timeToArrive))
    }
}
