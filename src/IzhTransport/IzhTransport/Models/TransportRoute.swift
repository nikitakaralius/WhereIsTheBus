//
//  TransportRoute.swift
//  IzhTransport
//
//  Created by Nikita Karalius on 22.05.2022.
//

import Foundation

struct TransportRoute: Codable {
    let transport: TransportType
    let number: Int
    let direction: Direction
}
