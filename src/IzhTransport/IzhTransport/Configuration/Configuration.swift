//
//  Configuration.swift
//  IzhTransport
//
//  Created by Nikita Karalius on 22.05.2022.
//

import Foundation

enum Configuration {
    enum Error: Swift.Error {
        case missingKey, invalidValue
    }

    static func value<T>(for key: String) throws -> T where T: LosslessStringConvertible {
           guard let object = Bundle.main.object(forInfoDictionaryKey: key) else {
               throw Error.missingKey
           }

           switch object {
           case let value as T:
               return value
           case let string as String:
               guard let value = T(string) else { fallthrough }
               return value
           default:
               throw Error.invalidValue
           }
       }
}

enum API {
    static var scheduleService: URL {
        let key = "Schedule Service"
        var address = "http://"
        do {
            address += try Configuration.value(for: key)
        } catch Configuration.Error.invalidValue {
            print("Value for the Schedule Service is not of type of string")
        } catch Configuration.Error.missingKey {
            print("Can not find \(key) key")
        } catch {
            print("An error occurred while getting \(key) value")
        }
        return URL(string: address)!
    }
}
