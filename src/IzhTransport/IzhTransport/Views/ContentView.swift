//
//  ContentView.swift
//  IzhTransport
//
//  Created by Nikita Karalius on 22.05.2022.
//

import SwiftUI

struct ContentView: View {
    let scheduleService = ScheduleService()

    @State private var text = "Hello, World!"

    var body: some View {
        Text(text)
            .padding()
            .onAppear {
                print(API.scheduleService)
                scheduleService.arrivals(for: TransportRoute(
                    transport: .bus,
                    number: 29,
                    direction: .direct)) { stops, error  in
                        text = stops.first?.name ?? "nil"
                        print(error ?? "no error")
                    }
            }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
