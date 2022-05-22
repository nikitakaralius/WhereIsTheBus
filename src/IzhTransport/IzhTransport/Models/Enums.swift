//
//  Direction.swift
//  IzhTransport
//
//  Created by Nikita Karalius on 22.05.2022.
//

import Foundation

enum Direction: Int, Codable {
    case none = 0, direct = 1, `return` = 2, both = 3
}

enum StrictDirection: Int, Codable {
    case none = 0, direct = 1, `return` = 2
}

enum TransportType: Int, Codable {
    case none = 0, bus = 1, trolleybus = 2, tram = 3
}

enum TimeToArrive {
    case minutes(Int), undefined(String)

    init(from value: String) {
        if let time = Int(value) {
            self = .minutes(time)
        } else {
            self = .undefined(value)
        }
    }
}
