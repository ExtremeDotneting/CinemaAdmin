--
-- ������ ������������ Devart dbForge Studio for MySQL, ������ 7.2.58.0
-- �������� �������� ��������: http://www.devart.com/ru/dbforge/mysql/studio
-- ���� �������: 30.04.2017 23:00:40
-- ������ �������: 5.7.18-log
-- ������ �������: 4.1
--


-- 
-- ���������� ������� ������
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- ���������� ����� SQL (SQL mode)
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- ��������� ���� ������ �� ���������
--
USE cinema_admin;

--
-- �������� ��� ������� cinema_rooms
--
DROP TABLE IF EXISTS cinema_rooms;
CREATE TABLE cinema_rooms (
  id INT(11) NOT NULL AUTO_INCREMENT,
  room_name VARCHAR(255) DEFAULT NULL,
  room_scheme_object TEXT DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB
AUTO_INCREMENT = 3
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
ROW_FORMAT = DYNAMIC;

--
-- �������� ��� ������� films
--
DROP TABLE IF EXISTS films;
CREATE TABLE films (
  id INT(11) NOT NULL AUTO_INCREMENT,
  film_name VARCHAR(50) DEFAULT NULL,
  description TEXT DEFAULT NULL,
  age_rate INT(11) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB
AUTO_INCREMENT = 8
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
ROW_FORMAT = DYNAMIC;

--
-- �������� ��� ������� sellers
--
DROP TABLE IF EXISTS sellers;
CREATE TABLE sellers (
  id INT(11) NOT NULL AUTO_INCREMENT,
  login VARCHAR(255) DEFAULT NULL,
  pass VARCHAR(255) DEFAULT NULL,
  name VARCHAR(50) DEFAULT NULL,
  rules_level TINYINT(4) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB
AUTO_INCREMENT = 2
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
ROW_FORMAT = DYNAMIC;

--
-- �������� ��� ������� seance_schedule
--
DROP TABLE IF EXISTS seance_schedule;
CREATE TABLE seance_schedule (
  id INT(11) NOT NULL AUTO_INCREMENT,
  film_id INT(11) DEFAULT NULL,
  room_id INT(11) DEFAULT NULL,
  seance_date DATETIME DEFAULT NULL,
  PRIMARY KEY (id),
  CONSTRAINT FK_seance_schedule_cinema_rooms_id FOREIGN KEY (room_id)
    REFERENCES cinema_rooms(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT FK_seance_schedule_films_id FOREIGN KEY (film_id)
    REFERENCES films(id) ON DELETE NO ACTION ON UPDATE NO ACTION
)
ENGINE = INNODB
AUTO_INCREMENT = 4
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
ROW_FORMAT = DYNAMIC;

--
-- �������� ��� ������� sold_tickets
--
DROP TABLE IF EXISTS sold_tickets;
CREATE TABLE sold_tickets (
  id INT(11) NOT NULL AUTO_INCREMENT,
  seance_id INT(11) DEFAULT NULL,
  seat_row TINYINT(4) DEFAULT NULL,
  seat_column TINYINT(4) DEFAULT NULL,
  sale_date DATETIME DEFAULT NULL,
  price INT(11) DEFAULT NULL,
  seller_id INT(11) DEFAULT NULL,
  PRIMARY KEY (id),
  CONSTRAINT FK_sold_tickets_seance_schedule_id FOREIGN KEY (seance_id)
    REFERENCES seance_schedule(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT FK_sold_tickets_sellers_id FOREIGN KEY (seller_id)
    REFERENCES sellers(id) ON DELETE NO ACTION ON UPDATE NO ACTION
)
ENGINE = INNODB
AUTO_INCREMENT = 41
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8
COLLATE utf8_general_ci
ROW_FORMAT = DYNAMIC;

-- 
-- ������������ ���������� ����� SQL (SQL mode)
-- 
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- ��������� ������� ������
-- 
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;