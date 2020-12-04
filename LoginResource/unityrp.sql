-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server Version:               8.0.13 - MySQL Community Server - GPL
-- Server Betriebssystem:        Win64
-- HeidiSQL Version:             11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Exportiere Datenbank Struktur für unityrp
CREATE DATABASE IF NOT EXISTS `unityrp` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_bin */;
USE `unityrp`;

-- Exportiere Struktur von Tabelle unityrp.user
CREATE TABLE IF NOT EXISTS `user` (
  `ID` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL DEFAULT '',
  `Passwort` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `SocialID` varchar(50) NOT NULL DEFAULT '',
  `WhitelistStatus` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Tabelle unityrp.user_character
CREATE TABLE IF NOT EXISTS `user_character` (
  `ID` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `UserID` int(11) unsigned NOT NULL,
  `X` double(10,5) DEFAULT NULL,
  `Y` double(10,5) DEFAULT NULL,
  `Z` double(10,5) DEFAULT NULL,
  `RX` double(10,5) DEFAULT NULL,
  `RY` double(10,5) DEFAULT NULL,
  `RZ` double(10,5) DEFAULT NULL,
  `CSlot1` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `FK_Character_UserID` (`UserID`),
  CONSTRAINT `FK_Character_UserID` FOREIGN KEY (`UserID`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Daten Export vom Benutzer nicht ausgewählt

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
