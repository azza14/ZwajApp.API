CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `AspNetRoles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(256) NULL,
    `NormalizedName` varchar(256) NULL,
    `ConcurrencyStamp` longtext NULL,
    CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetUsers` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserName` varchar(256) NULL,
    `NormalizedUserName` varchar(256) NULL,
    `Email` varchar(256) NULL,
    `NormalizedEmail` varchar(256) NULL,
    `EmailConfirmed` bit NOT NULL,
    `PasswordHash` longtext NULL,
    `SecurityStamp` longtext NULL,
    `ConcurrencyStamp` longtext NULL,
    `PhoneNumber` longtext NULL,
    `PhoneNumberConfirmed` bit NOT NULL,
    `TwoFactorEnabled` bit NOT NULL,
    `LockoutEnd` datetime(6) NULL,
    `LockoutEnabled` bit NOT NULL,
    `AccessFailedCount` int NOT NULL,
    `Gender` longtext NULL,
    `DateOfBirth` datetime(6) NOT NULL,
    `KnownAs` longtext NULL,
    `Created` datetime(6) NOT NULL,
    `LastActive` datetime(6) NOT NULL,
    `Introduction` longtext NULL,
    `LookingFor` longtext NULL,
    `Interests` longtext NULL,
    `City` longtext NULL,
    `Country` longtext NULL,
    CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
);

CREATE TABLE `Payments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `PaymentDate` datetime(6) NOT NULL,
    `Amount` double NOT NULL,
    `UserId` int NOT NULL,
    `ReceiptUrl` longtext NULL,
    `Description` longtext NULL,
    `Currency` longtext NULL,
    `IsPaid` bit NOT NULL,
    CONSTRAINT `PK_Payments` PRIMARY KEY (`Id`)
);

CREATE TABLE `Values` (
    `id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    CONSTRAINT `PK_Values` PRIMARY KEY (`id`)
);

CREATE TABLE `AspNetRoleClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RoleId` int NOT NULL,
    `ClaimType` longtext NULL,
    `ClaimValue` longtext NULL,
    CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `ClaimType` longtext NULL,
    `ClaimValue` longtext NULL,
    CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` varchar(255) NOT NULL,
    `ProviderKey` varchar(255) NOT NULL,
    `ProviderDisplayName` longtext NULL,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserRoles` (
    `UserId` int NOT NULL,
    `RoleId` int NOT NULL,
    CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserTokens` (
    `UserId` int NOT NULL,
    `LoginProvider` varchar(255) NOT NULL,
    `Name` varchar(255) NOT NULL,
    `Value` longtext NULL,
    CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Likes` (
    `LikerId` int NOT NULL,
    `LikeeId` int NOT NULL,
    CONSTRAINT `PK_Likes` PRIMARY KEY (`LikerId`, `LikeeId`),
    CONSTRAINT `FK_Likes_AspNetUsers_LikeeId` FOREIGN KEY (`LikeeId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Likes_AspNetUsers_LikerId` FOREIGN KEY (`LikerId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Messages` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `SenderId` int NOT NULL,
    `RecipientId` int NOT NULL,
    `Content` longtext NULL,
    `IsRead` bit NOT NULL,
    `DateRead` datetime(6) NULL,
    `MessageSent` datetime(6) NOT NULL,
    `SenderDeleted` bit NOT NULL,
    `RecipientDeleted` bit NOT NULL,
    CONSTRAINT `PK_Messages` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Messages_AspNetUsers_RecipientId` FOREIGN KEY (`RecipientId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Messages_AspNetUsers_SenderId` FOREIGN KEY (`SenderId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Photos` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Url` longtext NULL,
    `Description` longtext NULL,
    `DateAdded` datetime(6) NOT NULL,
    `IsMain` bit NOT NULL,
    `PublicId` longtext NULL,
    `UserId` int NOT NULL,
    `IsApproved` bit NOT NULL,
    CONSTRAINT `PK_Photos` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Photos_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

CREATE INDEX `IX_Likes_LikeeId` ON `Likes` (`LikeeId`);

CREATE INDEX `IX_Messages_RecipientId` ON `Messages` (`RecipientId`);

CREATE INDEX `IX_Messages_SenderId` ON `Messages` (`SenderId`);

CREATE INDEX `IX_Photos_UserId` ON `Photos` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190511234600_mysqlinitial', '2.1.4-rtm-31024');

INSERT INTO `AspNetRoles` (`Id`, `ConcurrencyStamp`, `Name`, `NormalizedName`)
VALUES (1, '633f463b-05f1-4e73-a597-4b6f8a6dae01', 'Admin', 'ADMIN');
INSERT INTO `AspNetRoles` (`Id`, `ConcurrencyStamp`, `Name`, `NormalizedName`)
VALUES (2, 'f05499f1-d879-448c-8bb9-b1370653f4d4', 'Moderator', 'MODERATOR');
INSERT INTO `AspNetRoles` (`Id`, `ConcurrencyStamp`, `Name`, `NormalizedName`)
VALUES (3, 'c8851061-179e-451c-8707-b8253c6969b8', 'Member', 'MEMBER');
INSERT INTO `AspNetRoles` (`Id`, `ConcurrencyStamp`, `Name`, `NormalizedName`)
VALUES (4, '54e9549e-8a97-40bd-a261-0d1105af0e55', 'VIP', 'VIP');

INSERT INTO `AspNetUsers` (`Id`, `AccessFailedCount`, `City`, `ConcurrencyStamp`, `Country`, `Created`, `DateOfBirth`, `Email`, `EmailConfirmed`, `Gender`, `Interests`, `Introduction`, `KnownAs`, `LastActive`, `LockoutEnabled`, `LockoutEnd`, `LookingFor`, `NormalizedEmail`, `NormalizedUserName`, `PasswordHash`, `PhoneNumber`, `PhoneNumberConfirmed`, `SecurityStamp`, `TwoFactorEnabled`, `UserName`)
VALUES (1, 0, NULL, '06504428-7b4c-4601-bad7-259e9be83651', NULL, '2019-05-17 15:07:24.142000', '1981-01-25 00:00:00.000000', 'admin@zwaj.com', TRUE, 'رجل', NULL, NULL, NULL, '2019-05-17 15:07:24.142000', FALSE, NULL, NULL, 'admin@zwaj.com', 'ADMIN', 'AQAAAAEAACcQAAAAEHu+0e31lUFUOTsS/HaI5dldrilio88B1WYZCPBjKazP3ywlmnOQfRBfHSO3lRrkXw==', NULL, FALSE, NULL, FALSE, 'admin');

INSERT INTO `AspNetUserRoles` (`UserId`, `RoleId`)
VALUES (1, 1);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190517130724_adminseed', '2.1.4-rtm-31024');

