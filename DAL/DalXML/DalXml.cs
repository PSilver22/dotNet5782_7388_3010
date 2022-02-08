#nullable enable
using DalApi;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DO;
using DalObject;

namespace DalXML
{
    public partial class DalXml : IDAL
    {
        private static readonly Lazy<DalXml> instance = new(() => new DalXml(@"..\..\..\..\DAL\Data\"));

        /// <summary>
        /// The path to the folder containing the XML files
        /// </summary>
        private readonly string _dataRootPath;

        /// <summary>
        /// The Instance for the singleton
        /// </summary>
        public static DalXml Instance => instance.Value;

        /// <summary>
        /// Initialize DalXml
        /// </summary>
        /// <param name="dataRootPath">The path to the folder containing the XML files</param>
        private DalXml(string dataRootPath)
        {
            _dataRootPath = dataRootPath;

            if (!System.IO.File.Exists(_dataRootPath + FileNames[File.config]))
            {
                new XElement("config",
                    new XElement("currentPackageId", 0),
                    new XElement("powerConsumption",
                        new XElement("free", 1),
                        new XElement("light", 2),
                        new XElement("mid", 3),
                        new XElement("heavy", 4)),
                    new XElement("chargeRate", 20)
                ).Save(_dataRootPath + FileNames[File.config]);
            }

            if (!System.IO.File.Exists(_dataRootPath + FileNames[File.customers]))
                new XElement("customers").Save(_dataRootPath + FileNames[File.customers]);
            if (!System.IO.File.Exists(_dataRootPath + FileNames[File.drones]))
                new XElement("drones").Save(_dataRootPath + FileNames[File.drones]);
            if (!System.IO.File.Exists(_dataRootPath + FileNames[File.droneCharges]))
                new XElement("droneCharges").Save(_dataRootPath + FileNames[File.droneCharges]);
            if (!System.IO.File.Exists(_dataRootPath + FileNames[File.packages]))
                new XElement("packages").Save(_dataRootPath + FileNames[File.packages]);
            if (!System.IO.File.Exists(_dataRootPath + FileNames[File.stations]))
                new XElement("stations").Save(_dataRootPath + FileNames[File.stations]);
        }

        public enum File
        {
            config,
            customers,
            drones,
            droneCharges,
            packages,
            stations
        }

        private static readonly Dictionary<File, string> FileNames = new()
        {
            [File.config] = "Config.xml",
            [File.customers] = "Customers.xml",
            [File.drones] = "Drones.xml",
            [File.droneCharges] = "DroneCharges.xml",
            [File.packages] = "Packages.xml",
            [File.stations] = "Stations.xml",
        };

        private XElement LoadFile(File file)
        {
            var filePath = _dataRootPath + FileNames[file];
            return XElement.Load(filePath);
        }

        private void SaveFile(File file, XElement root)
        {
            var filePath = _dataRootPath + FileNames[file];
            root.Save(filePath);
        }
    }
}