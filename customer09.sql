CREATE TABLE IF NOT EXISTS `cus_customer` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CustomerName` varchar(100) NOT NULL,
  `CustomerTypeId` int(11) DEFAULT NULL,
  `DOB` date DEFAULT NULL,
  `TaxCode` varchar(12) DEFAULT NULL,
  `PhoneNumber` varchar(20) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Address` varchar(200) NOT NULL,
  `Remark` varchar(500) DEFAULT NULL,
  `IsPublished` bit NOT NULL DEFAULT False,
  `DeleteFlag` bit NOT NULL DEFAULT False,
  `CreateDate` date NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `UpdateDate` date NOT NULL,
  `UpdatedBy` varchar(30) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_customer_customertype` (`CustomerTypeId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;
CREATE TABLE IF NOT EXISTS `cus_customertype` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CustomerTypeName` varchar(50) NOT NULL,
  `Description` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;
ALTER TABLE `cus_customer`
  ADD CONSTRAINT `FK_customer_customertype` FOREIGN KEY (`CustomerTypeId`) REFERENCES `cus_customertype` (`ID`);