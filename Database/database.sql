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

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
