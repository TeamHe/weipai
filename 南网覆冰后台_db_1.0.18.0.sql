-- MySQL dump 10.13  Distrib 8.0.11, for Win64 (x86_64)
--
-- Host: localhost    Database: picture
-- ------------------------------------------------------
-- Server version	8.0.11

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `t_admin`
--

DROP TABLE IF EXISTS `t_admin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_admin` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `pwd` varchar(255) NOT NULL,
  `LimitLevel` int(5) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_data_picture`
--

DROP TABLE IF EXISTS `t_data_picture`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_data_picture` (
  `idt_data_Picture` int(11) NOT NULL AUTO_INCREMENT,
  `time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `PoleID` int(11) NOT NULL,
  `Path` varchar(260) DEFAULT NULL COMMENT '图片路径\n',
  `state` enum('Success','Fail') DEFAULT NULL,
  `ChannalNO` int(11) DEFAULT NULL,
  `Presetting_No` int(11) DEFAULT NULL,
  PRIMARY KEY (`idt_data_Picture`),
  KEY `PoleID` (`PoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=116163 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_line`
--

DROP TABLE IF EXISTS `t_line`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_line` (
  `idt_line` int(11) NOT NULL AUTO_INCREMENT COMMENT '线路ID',
  `ID_Line` varchar(17) DEFAULT NULL COMMENT '线路ID',
  `Name_Line` varchar(45) DEFAULT NULL COMMENT '线路名称',
  PRIMARY KEY (`idt_line`),
  UNIQUE KEY `idt_line_UNIQUE` (`idt_line`),
  UNIQUE KEY `Name_Line_UNIQUE` (`Name_Line`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8 COMMENT='线路列表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_nw_error`
--

DROP TABLE IF EXISTS `t_nw_error`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_nw_error` (
  `idt_nw_error` int(11) NOT NULL AUTO_INCREMENT,
  `time` datetime NOT NULL,
  `poleid` int(11) NOT NULL,
  `up_time` datetime DEFAULT NULL,
  `func_code` int(11) DEFAULT NULL,
  `error_code` int(11) DEFAULT NULL,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`idt_nw_error`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='设备故障信息';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_nw_pull`
--

DROP TABLE IF EXISTS `t_nw_pull`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_nw_pull` (
  `idt_nw_pull` int(11) NOT NULL AUTO_INCREMENT,
  `time` datetime NOT NULL,
  `poleid` int(11) NOT NULL,
  `up_time` datetime DEFAULT NULL,
  `unit` int(11) NOT NULL COMMENT '功能单元识别码',
  `pull_max_pull` int(11) DEFAULT NULL COMMENT '最大拉力时刻-拉力',
  `angleDec_max_pull` double DEFAULT NULL COMMENT '最大拉力时刻-风偏角',
  `angleInc_max_pull` double DEFAULT NULL COMMENT '最大拉力时刻-倾斜角',
  `pull_min_pull` int(11) DEFAULT NULL COMMENT '最小拉力时刻-拉力',
  `angleDec_min_pull` double DEFAULT NULL COMMENT '最小拉力时刻-风偏角',
  `angleInc_min_pull` double DEFAULT NULL COMMENT ' 最小拉力时刻-倾斜角',
  `pull_max_angle` int(11) DEFAULT NULL COMMENT '最大风偏角时刻-拉力',
  `angleDec_max_angle` double DEFAULT NULL COMMENT '最大风偏角时刻-风偏角',
  `angleInc_max_angle` double DEFAULT NULL COMMENT '最大风偏角时刻-倾斜角',
  `pull_min_angle` int(11) DEFAULT NULL COMMENT '最小风偏角时刻-拉力',
  `angleDec_min_angle` double DEFAULT NULL COMMENT '最小风偏角时刻-风偏角',
  `angleInc_min_angle` double DEFAULT NULL COMMENT '最小风偏角时刻-倾斜角',
  PRIMARY KEY (`idt_nw_pull`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='南网导地线拉力及偏角数据';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_nw_weather`
--

DROP TABLE IF EXISTS `t_nw_weather`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_nw_weather` (
  `idt_nw_weather` int(11) NOT NULL AUTO_INCREMENT,
  `time` datetime NOT NULL,
  `poleid` int(11) NOT NULL,
  `up_time` datetime DEFAULT NULL,
  `temp` double DEFAULT NULL COMMENT '温度',
  `humidity` int(11) DEFAULT NULL COMMENT '湿度',
  `speed` double DEFAULT NULL COMMENT ' 瞬时风速',
  `direction` int(11) DEFAULT NULL COMMENT ' 瞬时风向',
  `rain` double DEFAULT NULL COMMENT '降雨量',
  `pressure` int(11) DEFAULT NULL COMMENT '大气压力',
  `sun` int(11) DEFAULT NULL COMMENT '日照',
  `speed_1_min` double DEFAULT NULL COMMENT '1分钟平均风速',
  `direction_1_min` int(11) DEFAULT NULL COMMENT '1分钟平均风向',
  `speed_10_min` double DEFAULT NULL COMMENT '10分钟平均风速',
  `direction_10_min` int(11) DEFAULT NULL COMMENT '10分钟平均风向',
  `speed_max` double DEFAULT NULL COMMENT '10分钟最大风速',
  PRIMARY KEY (`idt_nw_weather`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='南网微气象数据表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_powerpole`
--

DROP TABLE IF EXISTS `t_powerpole`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_powerpole` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL COMMENT '设备名称',
  `equNumber` varchar(45) NOT NULL COMMENT '设备编号\n',
  `CMD_ID` varchar(17) NOT NULL COMMENT '装置ID',
  `phone` varchar(20) DEFAULT NULL,
  `state` tinyint(4) DEFAULT NULL,
  `towerID` int(11) DEFAULT NULL,
  `urlID` int(11) DEFAULT NULL,
  `marketText` varchar(64) DEFAULT NULL COMMENT '水印文字',
  `updateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `type` int(11) DEFAULT NULL,
  `is_time` tinyint(1) DEFAULT NULL,
  `is_name` tinyint(1) DEFAULT NULL,
  `flag` varchar(32) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `CMD_ID_UNIQUE` (`CMD_ID`),
  UNIQUE KEY `equNumber_UNIQUE` (`equNumber`),
  KEY `fk_upload_idx` (`urlID`),
  KEY `tower_idx` (`towerID`),
  CONSTRAINT `tower` FOREIGN KEY (`towerID`) REFERENCES `t_tower` (`idt_tower`)
) ENGINE=InnoDB AUTO_INCREMENT=2480 DEFAULT CHARSET=utf8 COMMENT='设备列表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_tower`
--

DROP TABLE IF EXISTS `t_tower`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_tower` (
  `idt_tower` int(11) NOT NULL AUTO_INCREMENT,
  `TowerID` varchar(17) DEFAULT NULL COMMENT '杆塔ID',
  `TowerName` varchar(45) NOT NULL COMMENT '杆塔名称',
  `LineID` int(5) DEFAULT NULL COMMENT '线路ID',
  PRIMARY KEY (`idt_tower`),
  KEY `line_idx` (`LineID`)
) ENGINE=InnoDB AUTO_INCREMENT=1721 DEFAULT CHARSET=utf8 COMMENT='杆塔数据\r\n';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `t_upload`
--

DROP TABLE IF EXISTS `t_upload`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_upload` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `url` varchar(128) DEFAULT NULL,
  `is_time` tinyint(1) DEFAULT NULL,
  `is_name` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-04-16 21:57:33
