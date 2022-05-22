//
//  TransportStop.swift
//  IzhTransport
//
//  Created by Nikita Karalius on 22.05.2022.
//

import Foundation

struct TransportStop {
    let id: Int
    let name: String
    let direction: StrictDirection
    let timeToArrive: TimeToArrive
}
