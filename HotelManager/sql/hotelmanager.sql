-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 16, 2019 at 11:04 AM
-- Server version: 10.1.38-MariaDB
-- PHP Version: 7.3.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `hotelmanager`
--

-- --------------------------------------------------------

--
-- Table structure for table `customer`
--

CREATE TABLE `customer` (
  `id` int(11) NOT NULL,
  `name` varchar(128) CHARACTER SET utf8 NOT NULL,
  `id_card_number` bigint(20) NOT NULL,
  `address` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `type` varchar(20) CHARACTER SET utf8 NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `customer`
--

INSERT INTO `customer` (`id`, `name`, `id_card_number`, `address`, `type`) VALUES
(1, 'Nguyễn Văn Tuấn', 2511252515, 'Hà Nội', 'Inland');

-- --------------------------------------------------------

--
-- Table structure for table `customer_surcharge`
--

CREATE TABLE `customer_surcharge` (
  `quantum` int(11) NOT NULL,
  `surcharge` float NOT NULL,
  `note` text CHARACTER SET utf8 NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `customer_surcharge`
--

INSERT INTO `customer_surcharge` (`quantum`, `surcharge`, `note`) VALUES
(1, 2, 'Nhiều hơn 1 khách so với số khách tối đa của phòng'),
(2, 2.5, 'Nhiều hơn 2 khách so với số khách tối đa của phòng'),
(3, 3, 'Nhiều hơn 3 khách so với số khách tối đa của phòng');

-- --------------------------------------------------------

--
-- Table structure for table `customer_type`
--

CREATE TABLE `customer_type` (
  `type` varchar(20) CHARACTER SET utf8 NOT NULL,
  `surcharge` float NOT NULL,
  `note` text CHARACTER SET utf8 NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `customer_type`
--

INSERT INTO `customer_type` (`type`, `surcharge`, `note`) VALUES
('Foreign', 10, 'Khách nước ngoài'),
('Inland', 3, 'Khách trong nước');

-- --------------------------------------------------------

--
-- Table structure for table `rent_info`
--

CREATE TABLE `rent_info` (
  `id` int(11) NOT NULL,
  `room_id` int(11) NOT NULL,
  `customer_id` int(11) NOT NULL,
  `staff_id` int(11) NOT NULL,
  `checkin_date` date NOT NULL,
  `checkout_date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `room`
--

CREATE TABLE `room` (
  `id` int(11) NOT NULL,
  `name` varchar(128) NOT NULL,
  `type` varchar(128) NOT NULL,
  `status` enum('Available','NotAvailable') NOT NULL,
  `note` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `room`
--

INSERT INTO `room` (`id`, `name`, `type`, `status`, `note`) VALUES
(1, 'B777', 'B', 'Available', ''),
(2, 'A999', 'A', 'Available', 'dịch vụ vip'),
(3, 'C199', 'C', 'Available', ''),
(4, 'A111', 'A', 'Available', ''),
(5, 'B123', 'B', 'Available', '');

-- --------------------------------------------------------

--
-- Table structure for table `room_type`
--

CREATE TABLE `room_type` (
  `type` varchar(128) NOT NULL,
  `price` bigint(20) DEFAULT NULL,
  `note` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `room_type`
--

INSERT INTO `room_type` (`type`, `price`, `note`) VALUES
('A', 150000, ''),
('B', 170000, ''),
('C', 200000, '');

-- --------------------------------------------------------

--
-- Table structure for table `staff`
--

CREATE TABLE `staff` (
  `id` int(11) NOT NULL,
  `username` varchar(128) NOT NULL,
  `password` varchar(60) NOT NULL COMMENT 'password hash, using bcrypt',
  `fullname` varchar(254) NOT NULL,
  `lastLoginDate` datetime DEFAULT NULL,
  `createdDate` datetime DEFAULT NULL,
  `level` int(1) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `staff`
--

INSERT INTO `staff` (`id`, `username`, `password`, `fullname`, `lastLoginDate`, `createdDate`, `level`) VALUES
(1, 'admin', '$2a$10$7mI8PvVZq6dd7nSO/QUnfuJawKYIkYNoFLZig.wzU8GdN6rgIfPK2', 'Pro', '2019-04-18 00:00:00', '2019-04-18 00:00:00', 3),
(3, 'UK', '$2a$10$Bsq83vJ6s/h2Qtyqp4wePOO.C4tkQCnjBn6/RmB3cEt6ZsG4ogjae', 'Phạm Trần Chính', NULL, '2019-05-14 17:57:27', 1),
(4, 'Vip', '$2a$10$RZ9viEVxv9Q5I9Q8vbk7hODdYaqT6Pmn7PVgouOSQRtVwRrLsa88m', 'Lợi', NULL, '2019-05-15 17:57:44', 2);

-- --------------------------------------------------------

--
-- Table structure for table `staff_type`
--

CREATE TABLE `staff_type` (
  `level` int(11) NOT NULL,
  `type` varchar(128) CHARACTER SET utf8 NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `staff_type`
--

INSERT INTO `staff_type` (`level`, `type`) VALUES
(1, 'Receptionist'),
(2, 'Manager'),
(3, 'Administrator');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id_card_number` (`id_card_number`);

--
-- Indexes for table `room`
--
ALTER TABLE `room`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id_2` (`id`),
  ADD UNIQUE KEY `id_3` (`id`),
  ADD KEY `id_4` (`id`);

--
-- Indexes for table `staff`
--
ALTER TABLE `staff`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `customer`
--
ALTER TABLE `customer`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `room`
--
ALTER TABLE `room`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `staff`
--
ALTER TABLE `staff`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
