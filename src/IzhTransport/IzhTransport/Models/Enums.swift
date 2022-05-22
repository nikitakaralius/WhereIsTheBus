//
//  Direction.swift
//  IzhTransport
//
//  Created by Nikita Karalius on 22.05.2022.
//

import Foundation

enum Direction: Int {
    case none = 0, direct = 1, `return` = 2, both = 3
}

enum StrictDirection: Int {
    case none = 0, direct = 1, `return` = 2
}

enum TransportType: Int {
    case none = 0, bus = 1, trolleybus = 2, tram = 3
}

enum TimeToArrive {
    case minutes(Int), undefined(String)
}
