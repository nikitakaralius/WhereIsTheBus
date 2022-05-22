//
//  ContentView.swift
//  IzhTransport
//
//  Created by Nikita Karalius on 22.05.2022.
//

import SwiftUI

struct ContentView: View {
    var body: some View {
        Text("Hello, world!")
            .padding()
            .onAppear {
                print(API.scheduleService)
            }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
