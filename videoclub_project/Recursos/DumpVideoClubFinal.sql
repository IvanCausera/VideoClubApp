-- MySQL dump 10.13  Distrib 8.0.12, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: videoclub
-- ------------------------------------------------------
-- Server version	8.0.14

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
-- Table structure for table `actores_peliculas`
--

DROP TABLE IF EXISTS `actores_peliculas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `actores_peliculas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_pelicula` int(11) NOT NULL,
  `nombre` varchar(20) DEFAULT NULL,
  `apellido1` varchar(20) DEFAULT NULL,
  `apellido2` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_peliculas_actores_idx` (`id_pelicula`),
  CONSTRAINT `id_peliculas_actores` FOREIGN KEY (`id_pelicula`) REFERENCES `peliculas` (`idPeliculas`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `actores_peliculas`
--

LOCK TABLES `actores_peliculas` WRITE;
/*!40000 ALTER TABLE `actores_peliculas` DISABLE KEYS */;
INSERT INTO `actores_peliculas` VALUES (1,1,'Robert','Pattinson',NULL),(3,1,'Jeffrey','Wright',NULL),(10,31,'Toshirô Mifune',NULL,NULL),(11,31,' Takashi Shimura',NULL,NULL),(12,31,'Keiko Tsushima',NULL,NULL),(13,32,'Jim Carrey',NULL,NULL),(14,32,'Kate Winslet',NULL,NULL),(15,32,'Elijah Wood',NULL,NULL),(16,34,'Timothée Chalamet',NULL,NULL),(17,34,'Zendaya',NULL,NULL),(18,34,'Jason Momoa',NULL,NULL),(19,1,'Zoe Kravitz',NULL,NULL);
/*!40000 ALTER TABLE `actores_peliculas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `alquileres`
--

DROP TABLE IF EXISTS `alquileres`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `alquileres` (
  `idAlquileres` int(11) NOT NULL AUTO_INCREMENT,
  `id_cliente` int(11) NOT NULL,
  `fecha` date DEFAULT NULL,
  `fecha_devolucion` date DEFAULT NULL,
  `id_tipo_alquiler` int(11) NOT NULL,
  `reserva` date DEFAULT NULL,
  PRIMARY KEY (`idAlquileres`),
  KEY `id_tipo_alquiler_idx` (`id_tipo_alquiler`),
  KEY `id_cliente_alquiler_idx` (`id_cliente`),
  CONSTRAINT `id_cliente_alquiler` FOREIGN KEY (`id_cliente`) REFERENCES `cliente` (`idCliente`),
  CONSTRAINT `id_tipo_alquiler_alquiler` FOREIGN KEY (`id_tipo_alquiler`) REFERENCES `tipo_alquiler` (`idTipo_alquiler`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `alquileres`
--

LOCK TABLES `alquileres` WRITE;
/*!40000 ALTER TABLE `alquileres` DISABLE KEYS */;
INSERT INTO `alquileres` VALUES (14,4,'2022-06-07','2022-06-14',2,NULL),(15,27,'2022-06-06','2022-06-08',1,NULL),(16,23,'2022-06-08',NULL,2,NULL),(17,28,'2022-06-09',NULL,2,NULL),(18,30,'2022-06-07',NULL,2,NULL),(19,22,'2022-06-08',NULL,1,NULL),(20,20,'2022-03-14','2022-03-16',1,NULL),(21,4,'2022-03-22','2022-03-25',1,NULL),(22,28,'2022-03-28','2022-03-30',1,NULL),(23,20,'2022-04-13','2022-04-20',2,NULL),(24,21,'2022-04-08','2022-04-15',2,NULL),(25,4,'2022-06-06','2022-06-08',1,NULL),(26,4,'2022-06-08',NULL,1,NULL),(27,4,'2022-05-30',NULL,2,NULL);
/*!40000 ALTER TABLE `alquileres` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cliente`
--

DROP TABLE IF EXISTS `cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `cliente` (
  `idCliente` int(11) NOT NULL,
  `numero_tarjeta` varchar(25) DEFAULT NULL,
  PRIMARY KEY (`idCliente`),
  CONSTRAINT `idCliente_usuario` FOREIGN KEY (`idCliente`) REFERENCES `usuarios` (`idUsuarios`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cliente`
--

LOCK TABLES `cliente` WRITE;
/*!40000 ALTER TABLE `cliente` DISABLE KEYS */;
INSERT INTO `cliente` VALUES (4,'DSF546SDF4'),(20,'4929195168956'),(21,'4539857656527'),(22,'4929514614995'),(23,'4916629204226'),(24,'4539024527569'),(25,'5400752289560519'),(26,'5358799909543427'),(27,'5539534871797624'),(28,'3096825196451867'),(29,'3528730160726500'),(30,'5495423717128008');
/*!40000 ALTER TABLE `cliente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `empleado`
--

DROP TABLE IF EXISTS `empleado`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `empleado` (
  `idEmpleado` int(11) NOT NULL,
  `salario` float NOT NULL DEFAULT '0',
  `cuanta_bancaria` varchar(24) DEFAULT NULL,
  PRIMARY KEY (`idEmpleado`),
  CONSTRAINT `idEmpleado_usuario` FOREIGN KEY (`idEmpleado`) REFERENCES `usuarios` (`idUsuarios`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `empleado`
--

LOCK TABLES `empleado` WRITE;
/*!40000 ALTER TABLE `empleado` DISABLE KEYS */;
INSERT INTO `empleado` VALUES (2,60000,'ES5220852474946056997980'),(31,2500,'ES9600814323132423226521'),(32,2250,'ES5400819962784479597349');
/*!40000 ALTER TABLE `empleado` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `formatos`
--

DROP TABLE IF EXISTS `formatos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `formatos` (
  `idFormatos` int(11) NOT NULL AUTO_INCREMENT,
  `formato` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`idFormatos`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formatos`
--

LOCK TABLES `formatos` WRITE;
/*!40000 ALTER TABLE `formatos` DISABLE KEYS */;
INSERT INTO `formatos` VALUES (9,'CD-ROM'),(10,'CD-R'),(11,'CD-RW'),(12,'DVD-ROM'),(13,'DVD-R'),(14,'DVD-RW'),(15,'HD-DVD'),(16,'Blu-Ray'),(17,'U-Blu-Ray');
/*!40000 ALTER TABLE `formatos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `formatos_peliculas`
--

DROP TABLE IF EXISTS `formatos_peliculas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `formatos_peliculas` (
  `id` int(11) NOT NULL,
  `id_formato` int(11) NOT NULL,
  `id_pelicula` int(11) NOT NULL,
  `stock` int(11) NOT NULL DEFAULT '0',
  `precio` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `id_formato_pelicula_idx` (`id_formato`),
  KEY `id_pelicula_formato_idx` (`id_pelicula`),
  CONSTRAINT `idItem_plataformaVideojuego` FOREIGN KEY (`id`) REFERENCES `item` (`idItem`),
  CONSTRAINT `id_formato_pelicula` FOREIGN KEY (`id_formato`) REFERENCES `formatos` (`idFormatos`),
  CONSTRAINT `id_pelicula_formato` FOREIGN KEY (`id_pelicula`) REFERENCES `peliculas` (`idPeliculas`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `formatos_peliculas`
--

LOCK TABLES `formatos_peliculas` WRITE;
/*!40000 ALTER TABLE `formatos_peliculas` DISABLE KEYS */;
INSERT INTO `formatos_peliculas` VALUES (1,16,1,10,30),(2,12,1,42,12),(14,17,31,0,20),(16,13,32,9,20),(17,15,32,9,30),(18,13,34,8,20),(19,15,34,7,25),(20,9,31,10,10);
/*!40000 ALTER TABLE `formatos_peliculas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `generos`
--

DROP TABLE IF EXISTS `generos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `generos` (
  `idGeneros` int(11) NOT NULL AUTO_INCREMENT,
  `genero` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`idGeneros`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `generos`
--

LOCK TABLES `generos` WRITE;
/*!40000 ALTER TABLE `generos` DISABLE KEYS */;
INSERT INTO `generos` VALUES (1,'Ciencia ficción '),(2,'Westerns'),(3,'Misterio'),(4,'Comedias'),(5,'Drama'),(6,'Acción'),(7,'Aventura'),(8,'Fantasía'),(9,'Terror'),(10,'Thriller'),(11,'Animación'),(12,'Crimen'),(13,'Romance'),(14,'Bélicos'),(15,'Histórico'),(16,'Documental'),(17,'Musical'),(18,'Deportivos'),(19,'Biográfica'),(20,'Noir'),(21,'Lucha'),(22,'Arcade'),(23,'Simulación'),(24,'Plataformas'),(25,'Shooter'),(26,'RPG'),(27,'MMORPG'),(28,'Estrategia'),(29,'MOBA'),(30,'SandBox');
/*!40000 ALTER TABLE `generos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `idiomas`
--

DROP TABLE IF EXISTS `idiomas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `idiomas` (
  `idIdiomas` int(11) NOT NULL AUTO_INCREMENT,
  `idioma` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`idIdiomas`)
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `idiomas`
--

LOCK TABLES `idiomas` WRITE;
/*!40000 ALTER TABLE `idiomas` DISABLE KEYS */;
INSERT INTO `idiomas` VALUES (23,'Búlgaro'),(24,'Alemán'),(25,'Checo'),(26,'Chino'),(27,'Danés'),(28,'Eslovaco'),(29,'Esloveno'),(30,'Español'),(31,'Estonio'),(32,'Finés'),(33,'Francés'),(34,'Griego'),(35,'Húngaro'),(36,'Inglés'),(37,'Italiano'),(38,'Japonés'),(39,'Letón'),(40,'Lituano'),(41,'Neerlandés'),(42,'Polaco'),(43,'Portugués'),(44,'Rumano'),(45,'Ruso'),(46,'Sueco');
/*!40000 ALTER TABLE `idiomas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `item`
--

DROP TABLE IF EXISTS `item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `item` (
  `idItem` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`idItem`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item`
--

LOCK TABLES `item` WRITE;
/*!40000 ALTER TABLE `item` DISABLE KEYS */;
INSERT INTO `item` VALUES (1),(2),(5),(6),(7),(8),(13),(14),(15),(16),(17),(18),(19),(20),(21),(22),(23),(24),(25),(26),(27),(28),(29);
/*!40000 ALTER TABLE `item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `peliculas`
--

DROP TABLE IF EXISTS `peliculas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `peliculas` (
  `idPeliculas` int(11) NOT NULL,
  `titulo_original` varchar(30) DEFAULT NULL,
  `pais` varchar(40) DEFAULT NULL,
  `duracion` int(11) NOT NULL DEFAULT '0',
  `sinopsis` mediumtext,
  `director` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`idPeliculas`),
  CONSTRAINT `idPelicula_producto` FOREIGN KEY (`idPeliculas`) REFERENCES `productos` (`idProductos`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `peliculas`
--

LOCK TABLES `peliculas` WRITE;
/*!40000 ALTER TABLE `peliculas` DISABLE KEYS */;
INSERT INTO `peliculas` VALUES (1,'The Batman','EE.UU',176,'Cuando Enigma, un sádico asesino en serie, comienza a asesinar a las principales figuras políticas de Gotham, Batman se ve obligado a investigar la corrupción oculta de la ciudad y a cuestionar la participación de su familia.','Matt Reeves'),(31,'Shichinin no samurai','Japón',207,'Un pueblo pobre atacado por bandidos recluta a siete samuráis desempleados para que les ayuden a defenderse.','Akira Kurosawa'),(32,'Eternal Sunshine','EE.UU',108,'Cuando su relación se deteriora, una pareja se somete a un proceso médico para borrar el uno al otro de su memoria.','Michel Gondry'),(34,'Dune: Part One','EE.UU',155,'Adaptación de la novela de ciencia ficción de Frank Herbert sobre el hijo de una familia noble que trata de vengarse de la muerte de su padre y al mismo tiempo salvar un planeta que se le ha encomendado proteger.','Denis Villeneuve');
/*!40000 ALTER TABLE `peliculas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `permisos`
--

DROP TABLE IF EXISTS `permisos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `permisos` (
  `idPermiso` int(11) NOT NULL AUTO_INCREMENT,
  `permiso` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`idPermiso`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `permisos`
--

LOCK TABLES `permisos` WRITE;
/*!40000 ALTER TABLE `permisos` DISABLE KEYS */;
/*!40000 ALTER TABLE `permisos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `permisos_roles`
--

DROP TABLE IF EXISTS `permisos_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `permisos_roles` (
  `idPermisos_roles` int(11) NOT NULL AUTO_INCREMENT,
  `id_permiso` int(11) NOT NULL,
  `id_rol` int(11) NOT NULL,
  PRIMARY KEY (`idPermisos_roles`),
  KEY `id_permiso_rol_idx` (`id_permiso`),
  KEY `id_rol_permiso_idx` (`id_rol`),
  CONSTRAINT `id_permiso_rol` FOREIGN KEY (`id_permiso`) REFERENCES `permisos` (`idPermiso`),
  CONSTRAINT `id_rol_permiso` FOREIGN KEY (`id_rol`) REFERENCES `roles` (`idRoles`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `permisos_roles`
--

LOCK TABLES `permisos_roles` WRITE;
/*!40000 ALTER TABLE `permisos_roles` DISABLE KEYS */;
/*!40000 ALTER TABLE `permisos_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `plataformas`
--

DROP TABLE IF EXISTS `plataformas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `plataformas` (
  `idPlataformas` int(11) NOT NULL AUTO_INCREMENT,
  `plataforma` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`idPlataformas`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `plataformas`
--

LOCK TABLES `plataformas` WRITE;
/*!40000 ALTER TABLE `plataformas` DISABLE KEYS */;
INSERT INTO `plataformas` VALUES (4,'PlayStation 3'),(5,'PlayStation 4'),(6,'Xbox One'),(7,'Xbox Series X'),(8,'Xbox Series S'),(9,'Switch'),(10,'Steam'),(11,'GOG'),(12,'Epic Games'),(13,'PlayStation 5');
/*!40000 ALTER TABLE `plataformas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `plataformas_videojuegos`
--

DROP TABLE IF EXISTS `plataformas_videojuegos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `plataformas_videojuegos` (
  `id` int(11) NOT NULL,
  `id_plataforma` int(11) NOT NULL,
  `id_videojuego` int(11) NOT NULL,
  `stock` int(11) NOT NULL DEFAULT '0',
  `precio` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `id_plataforma_videojuego_idx` (`id_plataforma`),
  KEY `id_videojuego_plataforma_idx` (`id_videojuego`),
  CONSTRAINT `idItem_formatoPelicula` FOREIGN KEY (`id`) REFERENCES `item` (`idItem`),
  CONSTRAINT `id_plataforma_videojuego` FOREIGN KEY (`id_plataforma`) REFERENCES `plataformas` (`idPlataformas`),
  CONSTRAINT `id_videojuego_plataforma` FOREIGN KEY (`id_videojuego`) REFERENCES `videojuegos` (`idVideojuegos`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `plataformas_videojuegos`
--

LOCK TABLES `plataformas_videojuegos` WRITE;
/*!40000 ALTER TABLE `plataformas_videojuegos` DISABLE KEYS */;
INSERT INTO `plataformas_videojuegos` VALUES (7,4,26,36,29),(8,5,26,70,49),(21,6,36,20,40),(22,5,36,23,49),(23,13,36,50,49),(24,10,37,10,39),(25,12,37,20,49),(26,11,37,20,49),(27,10,38,10,20),(28,12,38,14,24),(29,5,41,10,49);
/*!40000 ALTER TABLE `plataformas_videojuegos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productos`
--

DROP TABLE IF EXISTS `productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `productos` (
  `idProductos` int(11) NOT NULL AUTO_INCREMENT,
  `titulo` varchar(30) DEFAULT NULL,
  `fecha` date DEFAULT NULL,
  `portada` varchar(40) DEFAULT NULL,
  `nota` int(11) NOT NULL DEFAULT '0',
  `edad_nota` varchar(10) DEFAULT NULL,
  `id_genero` int(11) NOT NULL,
  `id_idioma` int(11) NOT NULL,
  `id_idiomaDoblado` int(11) NOT NULL,
  PRIMARY KEY (`idProductos`),
  KEY `id_genero_producto_idx` (`id_genero`),
  KEY `id_idioma_producto_idx` (`id_idioma`),
  KEY `id_idiomaDoblado_producto_idx` (`id_idiomaDoblado`),
  CONSTRAINT `id_genero_producto` FOREIGN KEY (`id_genero`) REFERENCES `generos` (`idGeneros`),
  CONSTRAINT `id_idiomaDoblado_producto` FOREIGN KEY (`id_idiomaDoblado`) REFERENCES `idiomas` (`idIdiomas`),
  CONSTRAINT `id_idioma_producto` FOREIGN KEY (`id_idioma`) REFERENCES `idiomas` (`idIdiomas`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos`
--

LOCK TABLES `productos` WRITE;
/*!40000 ALTER TABLE `productos` DISABLE KEYS */;
INSERT INTO `productos` VALUES (1,'Batman','2022-02-12','Batman.png',74,'+18',6,36,30),(26,'The Last of Us','2013-06-14','The Last of Us.png',98,'+18',6,36,30),(31,'Los siete samuráis','1954-04-26','Los siete samuráis.png',86,'A',6,38,30),(32,'¡Olvídate de mí!','2004-03-09','¡Olvídate de mí!.png',83,'13',13,36,30),(34,'Dune','2021-09-10','Dune.png',98,'12',7,36,30),(36,'Crash Bandicoot 4','2020-10-02','Crash Bandicoot 4.png',83,'E',24,36,30),(37,'Disco Elysium','2019-10-15','Disco Elysium.png',100,'Z',26,36,36),(38,'Soma','2015-09-22','Soma.png',84,'Z',9,36,36),(41,'Call Of Duty','2022-06-08','Call Of Duty.png',73,'RP',25,36,30);
/*!40000 ALTER TABLE `productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productos_alquiler`
--

DROP TABLE IF EXISTS `productos_alquiler`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `productos_alquiler` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_alquiler` int(11) NOT NULL,
  `id_producto` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_aluiler_prodcutos_idx` (`id_alquiler`),
  KEY `id_producto_alquiler_idx` (`id_producto`),
  CONSTRAINT `id_aluiler_prodcutos` FOREIGN KEY (`id_alquiler`) REFERENCES `alquileres` (`idAlquileres`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `id_producto_alquiler` FOREIGN KEY (`id_producto`) REFERENCES `item` (`idItem`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos_alquiler`
--

LOCK TABLES `productos_alquiler` WRITE;
/*!40000 ALTER TABLE `productos_alquiler` DISABLE KEYS */;
INSERT INTO `productos_alquiler` VALUES (18,14,7),(20,15,19),(21,16,8),(22,17,8),(23,18,8),(24,20,20),(25,21,20),(26,19,20),(27,22,20),(28,23,2),(29,24,2),(30,25,2),(31,25,19),(32,26,8),(33,27,17);
/*!40000 ALTER TABLE `productos_alquiler` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `roles` (
  `idRoles` int(11) NOT NULL AUTO_INCREMENT,
  `rol` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`idRoles`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'administrador'),(2,'empleado'),(3,'cliente');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tipo_alquiler`
--

DROP TABLE IF EXISTS `tipo_alquiler`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tipo_alquiler` (
  `idTipo_alquiler` int(11) NOT NULL AUTO_INCREMENT,
  `precio` float NOT NULL DEFAULT '0',
  `duracion` int(11) NOT NULL DEFAULT '1',
  `recargo` float NOT NULL DEFAULT '0',
  `nombre` varchar(28) DEFAULT NULL,
  PRIMARY KEY (`idTipo_alquiler`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipo_alquiler`
--

LOCK TABLES `tipo_alquiler` WRITE;
/*!40000 ALTER TABLE `tipo_alquiler` DISABLE KEYS */;
INSERT INTO `tipo_alquiler` VALUES (1,10,3,5,'Tres Días'),(2,20,7,15,'Una Semana');
/*!40000 ALTER TABLE `tipo_alquiler` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `usuarios` (
  `idUsuarios` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(20) DEFAULT NULL,
  `apellido1` varchar(20) DEFAULT NULL,
  `apellido2` varchar(20) DEFAULT NULL,
  `direccion` varchar(55) DEFAULT NULL,
  `mail` varchar(40) DEFAULT NULL,
  `telefono` varchar(13) DEFAULT NULL,
  `fecha_nacimiento` date DEFAULT NULL,
  `user` varchar(10) DEFAULT NULL,
  `password` varchar(50) DEFAULT NULL,
  `id_rol` int(11) NOT NULL,
  `DNI` varchar(9) DEFAULT NULL,
  PRIMARY KEY (`idUsuarios`),
  KEY `id_rol_idx` (`id_rol`),
  CONSTRAINT `id_rol_user` FOREIGN KEY (`id_rol`) REFERENCES `roles` (`idRoles`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (2,'Paul','Atreides',NULL,'Arrakis-Sietch Tabr-6549','paul@fremen.com','667272870','1965-11-05','admin','admin',1,'82521143H'),(4,'Mavis','Benavides','Varela','PO-DO-4444','Varela@gustr.com','673 910 321','1976-06-08','test','test',3,'25312451P'),(20,'Tom','Cruise',NULL,'--','cruise@gmail.com','635 249 602','1962-07-03','Mapother','asdfasdf8*',3,'21431183J'),(21,'Ewan','McGregor',NULL,'--','Ewan@gmail.com','741 278 009','1971-03-31','ObiWan','asdfasdf8*',3,'66492822Y'),(22,'Elizabeth','Olsen',NULL,'--','olsen@gmail.com','793 333 328','1989-02-16','Wanda','asdfasdf8*',3,'14798401V'),(23,'Benedict','Cumberbatch',NULL,'--','benedict@gmail.com','645 627 138','1976-07-19','Strange','asdfasdf8*',3,'15359157X'),(24,'Hideo','Kojima',NULL,'--','solidSnake@kojima.com','631 105 297','1963-08-24','Snake','asdfasdf8*',3,'64252419X'),(25,'Yoko','Taro',NULL,'--','A2@taro.com','686 567 933','1970-06-06','2B','adsfasdf8*',3,'88168822R'),(26,'Neil','Druckmann',NULL,'--','doctorUckmann@neil.com','734 696 212','1978-12-05','uckmann','asdfasdf8*',3,'62151336T'),(27,'Shigeru','Miyamoto',NULL,'--','mario@miyamoto.com','738 125 017','1952-11-16','Mario','adsfasdf8*',3,'Shigeru '),(28,'Toshihiro','Nagoshi',NULL,'--','kiryu@nagoshi.com','618 120 436','1965-06-17','Yakuza','asdfasdf8*',3,'69483691R'),(29,'Kazuma','Kiryu',NULL,'--','Kazuma@yakuza.com','798 931 085','1965-07-04','Kiryu','asdfasfd8*',3,'29273849R'),(30,'Goro','Majima',NULL,'--','Majima@yakuza.com','641 222 852','1965-10-13','Goro','asdfasfd8*',3,'06108655Q'),(31,'Ellie','Williams',NULL,'Boston-Jackson-9584','Ellie@lastofus.com','710 643 746','2013-06-14','ellie','ellie',2,'50354123Q'),(32,'Joel','Miller',NULL,'Texas-Austin-3549','Joel@lastofus.com','693 035 704','1973-11-13','joel','joel',2,'37631767H');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ventas`
--

DROP TABLE IF EXISTS `ventas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ventas` (
  `idVentas` int(11) NOT NULL AUTO_INCREMENT,
  `id_cliente` int(11) NOT NULL,
  `fecha` date DEFAULT NULL,
  `reserva` date DEFAULT NULL,
  PRIMARY KEY (`idVentas`),
  KEY `id_cliente_venta_idx` (`id_cliente`),
  CONSTRAINT `id_cliente_venta` FOREIGN KEY (`id_cliente`) REFERENCES `cliente` (`idCliente`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ventas`
--

LOCK TABLES `ventas` WRITE;
/*!40000 ALTER TABLE `ventas` DISABLE KEYS */;
INSERT INTO `ventas` VALUES (19,24,'2022-06-08',NULL),(20,22,'2022-06-08',NULL),(21,21,'2022-05-11',NULL),(22,26,'2022-05-20',NULL),(23,24,'2022-03-11',NULL),(24,25,'2022-03-16',NULL),(25,29,'2022-03-19',NULL),(26,4,'2022-06-08',NULL),(27,4,'2022-06-08',NULL),(28,4,'2022-06-08',NULL);
/*!40000 ALTER TABLE `ventas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ventas_productos`
--

DROP TABLE IF EXISTS `ventas_productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ventas_productos` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_venta` int(11) NOT NULL,
  `id_producto` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_venta_idx` (`id_venta`),
  KEY `id_producto_venta_idx` (`id_producto`),
  CONSTRAINT `id_producto_venta` FOREIGN KEY (`id_producto`) REFERENCES `item` (`idItem`),
  CONSTRAINT `id_venta_ventaproductos` FOREIGN KEY (`id_venta`) REFERENCES `ventas` (`idVentas`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ventas_productos`
--

LOCK TABLES `ventas_productos` WRITE;
/*!40000 ALTER TABLE `ventas_productos` DISABLE KEYS */;
INSERT INTO `ventas_productos` VALUES (21,20,20),(22,19,8),(23,21,19),(24,22,19),(25,23,19),(26,24,18),(27,25,18),(28,26,8),(29,27,16),(30,28,18);
/*!40000 ALTER TABLE `ventas_productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `videojuegos`
--

DROP TABLE IF EXISTS `videojuegos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `videojuegos` (
  `idVideojuegos` int(11) NOT NULL,
  `distribuidora` varchar(25) DEFAULT NULL,
  `desarrolladora` varchar(25) DEFAULT NULL,
  `multijugador` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`idVideojuegos`),
  CONSTRAINT `idVideojuego_producto` FOREIGN KEY (`idVideojuegos`) REFERENCES `productos` (`idProductos`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `videojuegos`
--

LOCK TABLES `videojuegos` WRITE;
/*!40000 ALTER TABLE `videojuegos` DISABLE KEYS */;
INSERT INTO `videojuegos` VALUES (26,'Sony Interactive Entertai','Naughty Dog',0),(36,'Activision Blizzard','Toys for Bob',0),(37,'ZA/UM','ZA/UM',0),(38,'Frictional Games','Frictional Games',0),(41,'Activision Blizzard','Treyar',1);
/*!40000 ALTER TABLE `videojuegos` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-06-08 23:03:01
