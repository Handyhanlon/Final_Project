-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.11.11-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             12.10.0.7000
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for cpsy200_final
DROP DATABASE IF EXISTS `cpsy200_final`;
CREATE DATABASE IF NOT EXISTS `cpsy200_final` /*!40100 DEFAULT CHARACTER SET armscii8 COLLATE armscii8_bin */;
USE `cpsy200_final`;

-- Dumping structure for table cpsy200_final.customer
DROP TABLE IF EXISTS `customer`;
CREATE TABLE IF NOT EXISTS `customer` (
  `CustomerID` int(4) unsigned NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Phone` bigint(10) unsigned DEFAULT NULL,
  `Email` varchar(100) NOT NULL,
  PRIMARY KEY (`CustomerID`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Dumping data for table cpsy200_final.customer: ~5 rows (approximately)
DELETE FROM `customer`;
INSERT INTO `customer` (`CustomerID`, `FirstName`, `LastName`, `Phone`, `Email`) VALUES
	(1, 'John', 'Smith', 5551234567, 'john.smith@email.com'),
	(2, 'Sarah', 'Johnson', 5552345678, 'sarah.j@email.com'),
	(3, 'Michael', 'Williams', 5553456789, 'mike.w@email.com'),
	(4, 'Emily', 'Brown', 5554567890, 'emily.b@email.com'),
	(5, 'David', 'Jones', 5555678901, 'david.j@email.com');

-- Dumping structure for table cpsy200_final.equipment
DROP TABLE IF EXISTS `equipment`;
CREATE TABLE IF NOT EXISTS `equipment` (
  `EquipmentID` int(4) unsigned NOT NULL AUTO_INCREMENT,
  `CategoryID` int(2) unsigned NOT NULL,
  `CategoryName` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(100) DEFAULT NULL,
  `DailyRentalCost` double unsigned NOT NULL,
  PRIMARY KEY (`EquipmentID`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin COMMENT='Is all equipment entities';

-- Dumping data for table cpsy200_final.equipment: ~13 rows (approximately)
DELETE FROM `equipment`;
INSERT INTO `equipment` (`EquipmentID`, `CategoryID`, `CategoryName`, `Name`, `Description`, `DailyRentalCost`) VALUES
	(1, 10, 'Power tools', 'Drill Master 2000', 'Heavy-duty cordless drill with 2 batteries', 25),
	(2, 10, 'Power tools', 'Circular Saw Pro', '7-1/4 inch circular saw with laser guide', 30),
	(3, 10, 'Power tools', 'Impact Driver', '1/4 inch hex impact driver, 1800 in-lbs torque', 22.5),
	(4, 20, 'Yard equipment', 'Lawn Mower', '21-inch self-propelled gas lawn mower', 40),
	(5, 20, 'Yard equipment', 'Hedge Trimmer', '24-inch dual-action hedge trimmer', 28),
	(6, 20, 'Yard equipment', 'Leaf Blower', '200 MPH gas-powered leaf blower', 32),
	(7, 30, 'Compressors', 'Air Compressor 6gal', '6 gallon portable air compressor', 35),
	(8, 30, 'Compressors', 'Air Compressor 20gal', '20 gallon stationary air compressor', 50),
	(9, 40, 'Generators', 'Portable Generator 3500W', '3500 watt portable generator', 75),
	(10, 40, 'Generators', 'Standby Generator 7500W', '7500 watt standby generator', 120),
	(11, 50, 'Air tools', 'Air Impact Wrench', '1/2 inch drive impact wrench', 30),
	(12, 50, 'Air tools', 'Air Ratchet', '3/8 inch drive air ratchet', 25),
	(13, 50, 'Air tools', 'Air Nail Gun', 'Framing nail gun, 2-3.5 inch nails', 35);

-- Dumping structure for table cpsy200_final.equipment_rental
DROP TABLE IF EXISTS `equipment_rental`;
CREATE TABLE IF NOT EXISTS `equipment_rental` (
  `EquipmentID` int(4) unsigned NOT NULL,
  `RentalID` int(4) unsigned NOT NULL,
  PRIMARY KEY (`EquipmentID`,`RentalID`) USING BTREE,
  KEY `EquipRent_to_Rental` (`RentalID`),
  CONSTRAINT `EquipRent_to_Equip` FOREIGN KEY (`EquipmentID`) REFERENCES `equipment` (`EquipmentID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `EquipRent_to_Rental` FOREIGN KEY (`RentalID`) REFERENCES `rental` (`RentalID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Dumping data for table cpsy200_final.equipment_rental: ~5 rows (approximately)
DELETE FROM `equipment_rental`;
INSERT INTO `equipment_rental` (`EquipmentID`, `RentalID`) VALUES
	(1, 1),
	(4, 2),
	(7, 3),
	(10, 4),
	(12, 5);

-- Dumping structure for table cpsy200_final.rental
DROP TABLE IF EXISTS `rental`;
CREATE TABLE IF NOT EXISTS `rental` (
  `RentalID` int(4) unsigned NOT NULL AUTO_INCREMENT,
  `CurrentDate` date NOT NULL,
  `CustomerID` int(4) unsigned NOT NULL,
  `EquipmentID` int(4) unsigned NOT NULL,
  `RentalDate` date NOT NULL,
  `ReturnDate` date NOT NULL,
  PRIMARY KEY (`RentalID`),
  KEY `Rental_to_Customer` (`CustomerID`),
  CONSTRAINT `Rental_to_Customer` FOREIGN KEY (`CustomerID`) REFERENCES `customer` (`CustomerID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Dumping data for table cpsy200_final.rental: ~5 rows (approximately)
DELETE FROM `rental`;
INSERT INTO `rental` (`RentalID`, `CurrentDate`, `CustomerID`, `EquipmentID`, `RentalDate`, `ReturnDate`) VALUES
	(1, '2025-04-18', 1, 1, '2023-05-01', '2023-05-03'),
	(2, '2025-04-18', 2, 4, '2023-05-02', '2023-05-05'),
	(3, '2025-04-18', 3, 7, '2023-05-03', '2023-05-07'),
	(4, '2025-04-18', 4, 10, '2023-05-04', '2023-05-06'),
	(5, '2025-04-18', 5, 12, '2023-05-05', '2023-05-08');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
