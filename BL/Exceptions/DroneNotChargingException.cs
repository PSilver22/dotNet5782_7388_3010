﻿using System;

namespace IBL
{
    class DroneNotChargingException : Exception
    {
        public DroneNotChargingException() : base("Drone is not currently charging") { }
    }
}