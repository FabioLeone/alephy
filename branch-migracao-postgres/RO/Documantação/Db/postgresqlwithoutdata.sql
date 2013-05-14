--
-- Generated from mysql2pgsql.perl
-- http://gborg.postgresql.org/project/mysql2psql/
-- (c) 2001 - 2007 Jose M. Duarte, Joseph Speigle
--

-- warnings are printed for drop tables if they do not exist
-- please see http://archives.postgresql.org/pgsql-novice/2004-10/msg00158.php

-- ##############################################################
/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
-- MySQL dump 10.13  Distrib 5.6.11, for Win64 (x86_64)
--
-- Host: 187.1.133.25    Database: ro01
-- ------------------------------------------------------
-- Server version	5.5.29-log


--
-- Table structure for table arquivosenviados
--

DROP TABLE IF EXISTS "arquivosenviados" CASCADE ;
DROP SEQUENCE IF EXISTS "arquivosenviados_id_seq" CASCADE ;

CREATE SEQUENCE "arquivosenviados_id_seq"  START WITH 910143 ;

CREATE TABLE  "arquivosenviados" (
   "id" integer DEFAULT nextval('"arquivosenviados_id_seq"') NOT NULL,
   "data"   timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP , 
   "userid"   int DEFAULT NULL, 
   "cnpj"   varchar(255) DEFAULT NULL, 
   "tipo"   varchar(20) DEFAULT NULL, 
   "mes"   int DEFAULT NULL, 
   "ano"   int DEFAULT NULL, 
   primary key ("id")
)    ;
 CREATE OR REPLACE FUNCTION update_arquivosenviados() RETURNS trigger AS '
BEGIN
    NEW.data := CURRENT_TIMESTAMP; 
    RETURN NEW;
END;
' LANGUAGE 'plpgsql';

-- before INSERT is handled by 'default CURRENT_TIMESTAMP'
CREATE TRIGGER add_current_date_to_arquivosenviados BEFORE UPDATE ON "arquivosenviados" FOR EACH ROW EXECUTE PROCEDURE
update_arquivosenviados();
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table base_cliente_espera
--

DROP TABLE IF EXISTS "base_cliente_espera" CASCADE ;
DROP SEQUENCE IF EXISTS "base_cliente_espera_id_seq" CASCADE ;

CREATE SEQUENCE "base_cliente_espera_id_seq" ;

CREATE TABLE  "base_cliente_espera" (
   "id" integer DEFAULT nextval('"base_cliente_espera_id_seq"') NOT NULL,
   "razao_social"   varchar(255) DEFAULT NULL, 
   "cnpj"   varchar(255) NOT NULL DEFAULT '', 
   "mes"   int NOT NULL DEFAULT '0', 
   "ano"   int NOT NULL DEFAULT '0', 
   "barras"   varchar(255) NOT NULL DEFAULT '', 
   "descricao"   varchar(255) DEFAULT NULL, 
   "fabricante"   varchar(255) DEFAULT NULL, 
   "quantidade"   int DEFAULT NULL, 
   "valor_bruto"   decimal(12,2) DEFAULT NULL, 
   "valor_liquido"   decimal(12,2) DEFAULT NULL, 
   "valor_desconto"   decimal(12,2) DEFAULT NULL, 
   primary key ("id")
)    ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE INDEX "base_cliente_espera_barras_idx" ON "base_cliente_espera" USING btree ("barras");
CREATE INDEX "base_cliente_espera_1_idx" ON "base_cliente_espera" USING btree ("cnpj", "mes", "ano", "barras");

--
-- Table structure for table base_clientes
--

DROP TABLE IF EXISTS "base_clientes" CASCADE ;
DROP SEQUENCE IF EXISTS "base_clientes_id_seq" CASCADE ;

CREATE SEQUENCE "base_clientes_id_seq"  START WITH 22545013 ;

CREATE TABLE  "base_clientes" (
   "id" integer DEFAULT nextval('"base_clientes_id_seq"') NOT NULL,
   "razao_social"   varchar(255) DEFAULT NULL, 
   "cnpj"   varchar(255) NOT NULL DEFAULT '', 
   "mes"   int NOT NULL DEFAULT '0', 
   "ano"   int NOT NULL DEFAULT '0', 
   "barras"   varchar(255) NOT NULL DEFAULT '', 
   "descricao"   varchar(255) DEFAULT NULL, 
   "fabricante"   varchar(255) DEFAULT NULL, 
   "quantidade"   int DEFAULT NULL, 
   "valor_bruto"   decimal(12,2) DEFAULT NULL, 
   "valor_liquido"   decimal(12,2) DEFAULT NULL, 
   "valor_desconto"   decimal(12,2) DEFAULT NULL, 
   primary key ("id")
)    ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE INDEX "base_clientes_barras_idx" ON "base_clientes" USING btree ("barras");
CREATE INDEX "base_clientes_1_idx" ON "base_clientes" USING btree ("cnpj", "mes", "ano", "barras");

--
-- Table structure for table consolidado
--

DROP TABLE IF EXISTS "consolidado" CASCADE ;
DROP SEQUENCE IF EXISTS "consolidado_id_seq" CASCADE ;

CREATE SEQUENCE "consolidado_id_seq"  START WITH 320032 ;

CREATE TABLE  "consolidado" (
   "id" integer DEFAULT nextval('"consolidado_id_seq"') NOT NULL,
   "cnpj"   varchar(255) DEFAULT NULL, 
   "mes"   int DEFAULT NULL, 
   "ano"   int DEFAULT NULL, 
   "grupo"   varchar(50) DEFAULT NULL, 
   "sub_consultoria"   varchar(50) DEFAULT NULL, 
   "quantidade"   int DEFAULT NULL, 
   "valor_bruto"   decimal(12,2) DEFAULT NULL, 
   "valor_liquido"   decimal(12,2) DEFAULT NULL, 
   "valor_desconto"   decimal(12,2) DEFAULT NULL, 
   "importado"   bytea DEFAULT NULL, 
   primary key ("id")
)   ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table farmacias
--

DROP TABLE IF EXISTS "farmacias" CASCADE ;
DROP SEQUENCE IF EXISTS "farmacias_id_seq" CASCADE ;

CREATE SEQUENCE "farmacias_id_seq"  START WITH 1259 ;

CREATE TABLE  "farmacias" (
   "id" integer DEFAULT nextval('"farmacias_id_seq"') NOT NULL,
   "proprietarioid"   int DEFAULT NULL, 
   "gerenteid"   int DEFAULT NULL, 
   "email"   varchar(50) DEFAULT NULL, 
   "email2"   varchar(50) DEFAULT NULL, 
   "nomefantasia"   varchar(50) DEFAULT NULL, 
   "razaosocial"   varchar(50) DEFAULT NULL, 
   "cnpj"   varchar(30) DEFAULT '', 
   "endereco"   varchar(100) DEFAULT NULL, 
   "numero"   varchar(5) DEFAULT NULL, 
   "bairro"   varchar(20) DEFAULT NULL, 
   "complemento"   varchar(50) DEFAULT NULL, 
   "cidade"   varchar(50) DEFAULT NULL, 
   "uf"   int DEFAULT NULL, 
   "tel1"   varchar(20) DEFAULT NULL, 
   "tel2"   varchar(20) DEFAULT NULL, 
   "celular"   varchar(20) DEFAULT NULL, 
   "site"   varchar(30) DEFAULT NULL, 
   "skype"   varchar(30) DEFAULT NULL, 
   "msn"   varchar(30) DEFAULT NULL, 
   "ativo"   int DEFAULT NULL, 
   "idrede"   int DEFAULT NULL, 
   "cep"   varchar(10) DEFAULT NULL, 
   "proprietario"   varchar(255) DEFAULT NULL, 
   "gerente"   varchar(255) DEFAULT NULL, 
   primary key ("id")
)    ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE INDEX "farmacias_1_idx" ON "farmacias" USING btree ("proprietarioid", "cnpj", "idrede");

--
-- Table structure for table indice_relatorios
--

DROP TABLE IF EXISTS "indice_relatorios" CASCADE ;
DROP SEQUENCE IF EXISTS "indice_relatorios_id_seq" CASCADE ;

CREATE SEQUENCE "indice_relatorios_id_seq"  START WITH 11 ;

CREATE TABLE  "indice_relatorios" (
   "id" integer DEFAULT nextval('"indice_relatorios_id_seq"') NOT NULL,
   "grupo"   varchar(255) NOT NULL, 
   "categoria"   varchar(255) NOT NULL, 
   "venda"   decimal(3,2) NOT NULL, 
   "desconto"   decimal(3,2) NOT NULL, 
   primary key ("id")
)    ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table memberships
--

DROP TABLE IF EXISTS "memberships" CASCADE ;
DROP SEQUENCE IF EXISTS "memberships_userid_seq" CASCADE ;

CREATE SEQUENCE "memberships_userid_seq"  START WITH 413 ;

CREATE TABLE  "memberships" (
   "userid" integer DEFAULT nextval('"memberships_userid_seq"') NOT NULL,
   "password"   varchar(128) DEFAULT NULL, 
   "email"   varchar(255) DEFAULT NULL, 
   "inactive"   int DEFAULT NULL, 
   "createdate"   timestamp without time zone DEFAULT NULL, 
   "expirationdate"   timestamp without time zone DEFAULT NULL, 
   "access"   varchar(5) DEFAULT NULL, 
   "name"   varchar(200) DEFAULT NULL, 
   primary key ("userid")
)    ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table produtos_base
--

DROP TABLE IF EXISTS "produtos_base" CASCADE ;
CREATE TABLE  "produtos_base" (
   "codbarra"   varchar(50) NOT NULL DEFAULT '', 
   "codprod"   int DEFAULT NULL, 
   "nomeprod"   varchar(100) DEFAULT NULL, 
   "apresenta"   varchar(50) DEFAULT NULL, 
   "codlab"   varchar(50) DEFAULT NULL, 
   "nomelab"   varchar(100) DEFAULT NULL, 
   "codpat"   varchar(50) DEFAULT NULL, 
   "nomepat"   varchar(100) DEFAULT NULL, 
   "grupo"   varchar(50) DEFAULT NULL, 
   "sub_consultoria"   varchar(50) DEFAULT NULL, 
   "sub_divisao"   varchar(50) DEFAULT NULL, 
   "ncm"   varchar(20) DEFAULT NULL, 
   "NomePat-NCM" varchar(250) DEFAULT NULL,
   "descricao_ncm"   varchar(250) DEFAULT NULL, 
   "importado"   bytea DEFAULT NULL, 
   primary key ("codbarra")
)   ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE INDEX "produtos_base_codbarra_idx" ON "produtos_base" USING btree ("codbarra");

--
-- Table structure for table redesfarmaceuticas
--

DROP TABLE IF EXISTS "redesfarmaceuticas" CASCADE ;
DROP SEQUENCE IF EXISTS "redesfarmaceuticas_id_seq" CASCADE ;

CREATE SEQUENCE "redesfarmaceuticas_id_seq"  START WITH 17 ;

CREATE TABLE  "redesfarmaceuticas" (
   "id" integer DEFAULT nextval('"redesfarmaceuticas_id_seq"') NOT NULL,
   "descricao"   varchar(255) DEFAULT NULL, 
   "userid"   int DEFAULT NULL, 
   "cnpj"   varchar(18) DEFAULT NULL, 
   primary key ("id")
)    ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table relatorios
--

DROP TABLE IF EXISTS "relatorios" CASCADE ;
DROP SEQUENCE IF EXISTS "relatorios_relatorioid_seq" CASCADE ;

CREATE SEQUENCE "relatorios_relatorioid_seq"  START WITH 289 ;

CREATE TABLE  "relatorios" (
   "relatorioid" integer DEFAULT nextval('"relatorios_relatorioid_seq"') NOT NULL,
   "relatoriotipoid"   int DEFAULT NULL, 
   "usuarioid"   int DEFAULT NULL, 
   primary key ("relatorioid")
)    ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table relatorios_tipos
--

DROP TABLE IF EXISTS "relatorios_tipos" CASCADE ;
DROP SEQUENCE IF EXISTS "relatorios_tipos_relatoriotipoid_seq" CASCADE ;

CREATE SEQUENCE "relatorios_tipos_relatoriotipoid_seq"  START WITH 5 ;

CREATE TABLE  "relatorios_tipos" (
   "relatoriotipoid" integer DEFAULT nextval('"relatorios_tipos_relatoriotipoid_seq"') NOT NULL,
   "decricao"   varchar(100) DEFAULT NULL, 
   primary key ("relatoriotipoid")
)    ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table relatorios_visualizados
--

DROP TABLE IF EXISTS "relatorios_visualizados" CASCADE ;
DROP SEQUENCE IF EXISTS "relatorios_visualizados_relatorio_visualizadoid_seq" CASCADE ;

CREATE SEQUENCE "relatorios_visualizados_relatorio_visualizadoid_seq"  START WITH 364 ;

CREATE TABLE  "relatorios_visualizados" (
   "relatorio_visualizadoid" integer DEFAULT nextval('"relatorios_visualizados_relatorio_visualizadoid_seq"') NOT NULL,
   "userid"   int DEFAULT NULL, 
   "relatorio"   varchar(100) DEFAULT NULL, 
   "data"   timestamp without time zone DEFAULT NULL, 
   primary key ("relatorio_visualizadoid")
)   ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table roles
--

DROP TABLE IF EXISTS "roles" CASCADE ;
DROP SEQUENCE IF EXISTS "roles_roleid_seq" CASCADE ;

CREATE SEQUENCE "roles_roleid_seq"  START WITH 382 ;

CREATE TABLE  "roles" (
   "roleid" integer DEFAULT nextval('"roles_roleid_seq"') NOT NULL,
   "userid"   int DEFAULT NULL, 
   "envio"   bytea DEFAULT NULL, 
   "relatoriostodos"   bytea DEFAULT NULL, 
   primary key ("roleid")
)    ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table uf
--

DROP TABLE IF EXISTS "uf" CASCADE ;
CREATE TABLE  "uf" (
   "id"   int NOT NULL, 
   "uf"   varchar(3) DEFAULT NULL, 
   primary key ("id")
)   ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table users
--

DROP TABLE IF EXISTS "users" CASCADE ;
DROP SEQUENCE IF EXISTS "users_userid_seq" CASCADE ;

CREATE SEQUENCE "users_userid_seq"  START WITH 413 ;

CREATE TABLE  "users" (
   "userid" integer DEFAULT nextval('"users_userid_seq"') NOT NULL,
   "username"   varchar(255) DEFAULT NULL, 
   "lastactivitydate"   timestamp without time zone DEFAULT NULL, 
   "tipoid"   int DEFAULT NULL, 
   primary key ("userid")
)    ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table usuarios_farmacias
--

DROP TABLE IF EXISTS "usuarios_farmacias" CASCADE ;
DROP SEQUENCE IF EXISTS "usuarios_farmacias_id_seq" CASCADE ;

CREATE SEQUENCE "usuarios_farmacias_id_seq"  START WITH 31 ;

CREATE TABLE  "usuarios_farmacias" (
   "id" integer DEFAULT nextval('"usuarios_farmacias_id_seq"') NOT NULL,
   "userid"   int NOT NULL, 
   "farmaciaid"   int NOT NULL, 
   primary key ("id")
)    ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table usuarios_tipos
--

DROP TABLE IF EXISTS "usuarios_tipos" CASCADE ;
DROP SEQUENCE IF EXISTS "usuarios_tipos_id_seq" CASCADE ;

CREATE SEQUENCE "usuarios_tipos_id_seq"  START WITH 5 ;

CREATE TABLE  "usuarios_tipos" (
   "id" integer DEFAULT nextval('"usuarios_tipos_id_seq"') NOT NULL,
   "tipo"   varchar(100) DEFAULT NULL, 
   primary key ("id")
)   ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;

--
-- Table structure for table usuarios_vinculos
--

DROP TABLE IF EXISTS "usuarios_vinculos" CASCADE ;
DROP SEQUENCE IF EXISTS "usuarios_vinculos_id_seq" CASCADE ;

CREATE SEQUENCE "usuarios_vinculos_id_seq"  START WITH 3 ;

CREATE TABLE  "usuarios_vinculos" (
   "id" integer DEFAULT nextval('"usuarios_vinculos_id_seq"') NOT NULL,
   "usuarioid"   int NOT NULL, 
   "linkid"   int NOT NULL, 
   primary key ("id")
)   ;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
