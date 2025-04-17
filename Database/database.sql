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
CREATE DATABASE IF NOT EXISTS `cpsy200_final` /*!40100 DEFAULT CHARACTER SET armscii8 COLLATE armscii8_bin */;
USE `cpsy200_final`;

-- Dumping structure for table cpsy200_final.customer
CREATE TABLE IF NOT EXISTS `customer` (
  `CustomerID` int(4) unsigned NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Phone` bigint(10) unsigned DEFAULT NULL,
  `Email` varchar(100) NOT NULL,
  PRIMARY KEY (`CustomerID`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Dumping data for table cpsy200_final.customer: ~0 rows (approximately)
DELETE FROM `customer`;

-- Dumping structure for table cpsy200_final.equipment
CREATE TABLE IF NOT EXISTS `equipment` (
  `EquipmentID` int(4) unsigned NOT NULL AUTO_INCREMENT,
  `CategoryID` int(2) unsigned NOT NULL,
  `CategoryName` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(100) DEFAULT NULL,
  `DailyRentalCost` double unsigned NOT NULL,
  PRIMARY KEY (`EquipmentID`)
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin COMMENT='Is all equipment entities';

-- Dumping data for table cpsy200_final.equipment: ~0 rows (approximately)
DELETE FROM `equipment`;

-- Dumping structure for table cpsy200_final.equipment_rental
CREATE TABLE IF NOT EXISTS `equipment_rental` (
  `EquipmentID` int(4) unsigned NOT NULL,
  `RentalID` int(4) unsigned NOT NULL,
  PRIMARY KEY (`EquipmentID`,`RentalID`) USING BTREE,
  KEY `EquipRent_to_Rental` (`RentalID`),
  CONSTRAINT `EquipRent_to_Equip` FOREIGN KEY (`EquipmentID`) REFERENCES `equipment` (`EquipmentID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `EquipRent_to_Rental` FOREIGN KEY (`RentalID`) REFERENCES `rental` (`RentalID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Dumping data for table cpsy200_final.equipment_rental: ~0 rows (approximately)
DELETE FROM `equipment_rental`;

-- Dumping structure for table cpsy200_final.rental
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
) ENGINE=InnoDB DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

-- Dumping data for table cpsy200_final.rental: ~0 rows (approximately)
DELETE FROM `rental`;

-- Insert data into customer table
INSERT INTO `customer` (`FirstName`, `LastName`, `Phone`, `Email`) VALUES
('John', 'Smith', 5551234567, 'john.smith@email.com'),
('Sarah', 'Johnson', 5552345678, 'sarah.j@email.com'),
('Michael', 'Williams', 5553456789, 'mike.w@email.com'),
('Emily', 'Brown', 5554567890, 'emily.b@email.com'),
('David', 'Jones', 5555678901, 'david.j@email.com');

-- Insert equipment categories
INSERT INTO `equipment` (`CategoryID`, `CategoryName`, `Name`, `Description`, `DailyRentalCost`) VALUES
-- Power tools (Category 10)
(10, 'Power tools', 'Drill Master 2000', 'Heavy-duty cordless drill with 2 batteries', 25.00),
(10, 'Power tools', 'Circular Saw Pro', '7-1/4 inch circular saw with laser guide', 30.00),
(10, 'Power tools', 'Impact Driver', '1/4 inch hex impact driver, 1800 in-lbs torque', 22.50),

-- Yard equipment (Category 20)
(20, 'Yard equipment', 'Lawn Mower', '21-inch self-propelled gas lawn mower', 40.00),
(20, 'Yard equipment', 'Hedge Trimmer', '24-inch dual-action hedge trimmer', 28.00),
(20, 'Yard equipment', 'Leaf Blower', '200 MPH gas-powered leaf blower', 32.00),

-- Compressors (Category 30)
(30, 'Compressors', 'Air Compressor 6gal', '6 gallon portable air compressor', 35.00),
(30, 'Compressors', 'Air Compressor 20gal', '20 gallon stationary air compressor', 50.00),

-- Generators (Category 40)
(40, 'Generators', 'Portable Generator 3500W', '3500 watt portable generator', 75.00),
(40, 'Generators', 'Standby Generator 7500W', '7500 watt standby generator', 120.00),

-- Air tools (Category 50)
(50, 'Air tools', 'Air Impact Wrench', '1/2 inch drive impact wrench', 30.00),
(50, 'Air tools', 'Air Ratchet', '3/8 inch drive air ratchet', 25.00),
(50, 'Air tools', 'Air Nail Gun', 'Framing nail gun, 2-3.5 inch nails', 35.00);

-- Insert rental records
INSERT INTO `rental` (`CurrentDate`, `CustomerID`, `EquipmentID`, `RentalDate`, `ReturnDate`) VALUES
(CURDATE(), 1, 1, '2023-05-01', '2023-05-03'),
(CURDATE(), 2, 4, '2023-05-02', '2023-05-05'),
(CURDATE(), 3, 7, '2023-05-03', '2023-05-07'),
(CURDATE(), 4, 10, '2023-05-04', '2023-05-06'),
(CURDATE(), 5, 12, '2023-05-05', '2023-05-08');

-- Insert equipment_rental records (junction table)
INSERT INTO `equipment_rental` (`EquipmentID`, `RentalID`) VALUES
(1, 1),
(4, 2),
(7, 3),
(10, 4),
(12, 5);
/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
