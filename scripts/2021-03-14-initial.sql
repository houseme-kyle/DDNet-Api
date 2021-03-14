CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `Clan` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(16) NULL,
    `CountryCode` varchar(3) NULL,
    CONSTRAINT `PK_Clan` PRIMARY KEY (`Id`)
);

CREATE TABLE `DifficultyTier` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Difficulty` int NOT NULL,
    `Tier` int NOT NULL,
    `Points` int NOT NULL,
    CONSTRAINT `PK_DifficultyTier` PRIMARY KEY (`Id`)
);

CREATE TABLE `Pins` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Lookup` varchar(80) NULL,
    `Salt` varchar(30) NULL,
    `Instantiated` datetime(6) NOT NULL,
    CONSTRAINT `PK_Pins` PRIMARY KEY (`Id`)
);

CREATE TABLE `Server` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(16) NULL,
    `Region` varchar(3) NULL,
    `Difficulty` int NOT NULL,
    CONSTRAINT `PK_Server` PRIMARY KEY (`Id`)
);

CREATE TABLE `UserRole` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `Role` int NOT NULL,
    CONSTRAINT `PK_UserRole` PRIMARY KEY (`Id`)
);

CREATE TABLE `User` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ClanId` int NULL,
    `AccessKey` char(36) NOT NULL,
    `Name` varchar(16) NULL,
    `EmailHash` varchar(80) NULL,
    `CountryCode` varchar(3) NULL,
    `Timestamp` datetime(6) NOT NULL,
    CONSTRAINT `PK_User` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_User_Clan_ClanId` FOREIGN KEY (`ClanId`) REFERENCES `Clan` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Map` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `DifficultyTierId` int NOT NULL,
    `TierId` int NOT NULL,
    `Name` varchar(16) NULL,
    CONSTRAINT `PK_Map` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Map_DifficultyTier_DifficultyTierId` FOREIGN KEY (`DifficultyTierId`) REFERENCES `DifficultyTier` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Race` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `MapId` int NOT NULL,
    `UserId` int NOT NULL,
    `ServerId` int NOT NULL,
    `Timestamp` datetime(6) NOT NULL,
    `Time` decimal(65,30) NOT NULL,
    `Team` tinyint(1) NOT NULL,
    `RaceCode` varchar(36) NULL,
    CONSTRAINT `PK_Race` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Race_Map_MapId` FOREIGN KEY (`MapId`) REFERENCES `Map` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Race_Server_ServerId` FOREIGN KEY (`ServerId`) REFERENCES `Server` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Race_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `User` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `RaceCheckpoint` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RaceId` int NOT NULL,
    `Checkpoint` int NOT NULL,
    `Time` decimal(65,30) NOT NULL,
    CONSTRAINT `PK_RaceCheckpoint` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_RaceCheckpoint_Race_RaceId` FOREIGN KEY (`RaceId`) REFERENCES `Race` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Map_DifficultyTierId` ON `Map` (`DifficultyTierId`);

CREATE INDEX `IX_Race_MapId` ON `Race` (`MapId`);

CREATE INDEX `IX_Race_ServerId` ON `Race` (`ServerId`);

CREATE INDEX `IX_Race_UserId` ON `Race` (`UserId`);

CREATE INDEX `IX_RaceCheckpoint_RaceId` ON `RaceCheckpoint` (`RaceId`);

CREATE INDEX `IX_User_ClanId` ON `User` (`ClanId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210314225053_Initial', '5.0.4');

COMMIT;

