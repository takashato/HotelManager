-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th6 06, 2019 lúc 01:52 PM
-- Phiên bản máy phục vụ: 10.1.38-MariaDB
-- Phiên bản PHP: 7.3.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `hotelmanager`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `customer`
--

CREATE TABLE `customer` (
  `id` int(11) NOT NULL,
  `name` varchar(128) NOT NULL,
  `id_card_number` bigint(20) NOT NULL,
  `address` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `type` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Đang đổ dữ liệu cho bảng `customer`
--

INSERT INTO `customer` (`id`, `name`, `id_card_number`, `address`, `type`) VALUES
(1, 'UK', 123123, 'Nhật', 'Nước ngoài'),
(2, 'Huy', 51515, 'Hà Nội', 'Nội địa'),
(3, 'Nguyễn Ngọc', 6787, 'Đà Nẵng', 'Nội địa');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `customer_surcharge`
--

CREATE TABLE `customer_surcharge` (
  `quantum` int(11) NOT NULL,
  `surcharge` float NOT NULL,
  `note` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Đang đổ dữ liệu cho bảng `customer_surcharge`
--

INSERT INTO `customer_surcharge` (`quantum`, `surcharge`, `note`) VALUES
(1, 2, 'Nhiều hơn 1 khách so với số khách tối đa của phòng'),
(2, 2.5, 'Nhiều hơn 2 khách so với số khách tối đa của phòng'),
(3, 3, 'Nhiều hơn 3 khách so với số khách tối đa của phòng'),
(5, 4, '');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `customer_type`
--

CREATE TABLE `customer_type` (
  `type` varchar(20) NOT NULL,
  `surcharge` double NOT NULL,
  `note` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Đang đổ dữ liệu cho bảng `customer_type`
--

INSERT INTO `customer_type` (`type`, `surcharge`, `note`) VALUES
('Nước ngoài', 20, 'Khách nước ngoài đặc biệt'),
('Nội địa', 0, 'Khách trong nước');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `payment_detail`
--

CREATE TABLE `payment_detail` (
  `id` int(11) NOT NULL,
  `room_name` varchar(128) NOT NULL,
  `checkin_date` date NOT NULL,
  `days_rented` int(11) NOT NULL,
  `customer_quantum` int(11) NOT NULL,
  `foreign_quantum` int(11) NOT NULL,
  `amount` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Đang đổ dữ liệu cho bảng `payment_detail`
--

INSERT INTO `payment_detail` (`id`, `room_name`, `checkin_date`, `days_rented`, `customer_quantum`, `foreign_quantum`, `amount`) VALUES
(1, 'B777', '2019-06-06', 1, 1, 1, 204000),
(2, 'B123', '2019-06-06', 1, 1, 0, 170000),
(3, 'A999', '2019-06-06', 1, 3, 0, 150000);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `rent_info`
--

CREATE TABLE `rent_info` (
  `id` int(11) NOT NULL,
  `room_name` varchar(128) NOT NULL,
  `customer_id` bigint(20) NOT NULL,
  `staff_username` varchar(128) NOT NULL,
  `checkin_date` date NOT NULL,
  `checkout_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Đang đổ dữ liệu cho bảng `rent_info`
--

INSERT INTO `rent_info` (`id`, `room_name`, `customer_id`, `staff_username`, `checkin_date`, `checkout_date`) VALUES
(1, 'B777', 123123, 'admin', '2019-06-06', NULL),
(2, 'B123', 51515, 'admin', '2019-06-06', NULL),
(3, 'A999', 6787, 'admin', '2019-06-06', NULL);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `revenue_report`
--

CREATE TABLE `revenue_report` (
  `id` int(11) NOT NULL,
  `room_name` varchar(128) NOT NULL,
  `room_type` varchar(128) NOT NULL,
  `checkin_date` date NOT NULL,
  `checkout_date` date NOT NULL,
  `amount` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `room`
--

CREATE TABLE `room` (
  `id` int(11) NOT NULL,
  `name` varchar(128) NOT NULL,
  `type` varchar(128) NOT NULL,
  `status` varchar(128) NOT NULL,
  `note` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Đang đổ dữ liệu cho bảng `room`
--

INSERT INTO `room` (`id`, `name`, `type`, `status`, `note`) VALUES
(1, 'B777', 'B', 'NotAvailable', ''),
(2, 'A999', 'A', 'NotAvailable', 'dịch vụ vip'),
(3, 'C199', 'C', 'Available', ''),
(4, 'A111', 'A', 'Available', ''),
(5, 'B123', 'B', 'NotAvailable', '');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `room_rental_detail`
--

CREATE TABLE `room_rental_detail` (
  `id` int(11) NOT NULL,
  `room_name` varchar(128) NOT NULL,
  `customer_name` varchar(128) NOT NULL,
  `customer_id` bigint(20) DEFAULT NULL,
  `address` text NOT NULL,
  `customer_type` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Đang đổ dữ liệu cho bảng `room_rental_detail`
--

INSERT INTO `room_rental_detail` (`id`, `room_name`, `customer_name`, `customer_id`, `address`, `customer_type`) VALUES
(1, 'B777', 'UK', 123123, 'Nhật', 'Nước ngoài'),
(2, 'B123', 'Huy', 51515, 'Hà Nội', 'Nội địa'),
(3, 'A999', 'Nguyễn Ngọc', 6787, 'Đà Nẵng', 'Nội địa'),
(4, 'A999', 'Nguyễn Huy', 4543, 'Huế', 'Nội địa'),
(5, 'A999', 'Nguyễn Bảo', 97897, 'Hà Nội', 'Nội địa');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `room_type`
--

CREATE TABLE `room_type` (
  `type` varchar(128) NOT NULL,
  `price` bigint(20) DEFAULT NULL,
  `max_customer` int(11) NOT NULL,
  `note` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Đang đổ dữ liệu cho bảng `room_type`
--

INSERT INTO `room_type` (`type`, `price`, `max_customer`, `note`) VALUES
('A', 150000, 3, ''),
('B', 170000, 4, ''),
('C', 200000, 5, ''),
('D', 500000, 8, 'Vip');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `staff`
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
-- Đang đổ dữ liệu cho bảng `staff`
--

INSERT INTO `staff` (`id`, `username`, `password`, `fullname`, `lastLoginDate`, `createdDate`, `level`) VALUES
(1, 'admin', '$2a$10$7mI8PvVZq6dd7nSO/QUnfuJawKYIkYNoFLZig.wzU8GdN6rgIfPK2', 'Pro', '2019-04-18 00:00:00', '2019-04-18 00:00:00', 3),
(3, 'UK', '$2a$10$NNX54NJo4zQglVDiI.XUzeXx23UhODGIqw1Mj8O0U4zIfCddS/VOS', 'Phạm Trần Chính', NULL, '2019-05-14 17:57:27', 2),
(4, 'Vip', '$2a$10$RZ9viEVxv9Q5I9Q8vbk7hODdYaqT6Pmn7PVgouOSQRtVwRrLsa88m', 'Lợi', NULL, '2019-05-15 17:57:44', 2),
(5, 'Pro', '$2a$10$U.zKMmU88QghiDj8G4CTYu4HwbXy6wRDkPHNLpmtll3hVLAA3CURu', 'Dũng', NULL, '2019-05-18 15:56:50', 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `staff_type`
--

CREATE TABLE `staff_type` (
  `level` int(11) NOT NULL,
  `type` varchar(128) CHARACTER SET utf8 NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Đang đổ dữ liệu cho bảng `staff_type`
--

INSERT INTO `staff_type` (`level`, `type`) VALUES
(1, 'Receptionist'),
(2, 'Manager'),
(3, 'Administrator');

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id_card_number` (`id_card_number`);

--
-- Chỉ mục cho bảng `payment_detail`
--
ALTER TABLE `payment_detail`
  ADD PRIMARY KEY (`id`);

--
-- Chỉ mục cho bảng `rent_info`
--
ALTER TABLE `rent_info`
  ADD PRIMARY KEY (`id`);

--
-- Chỉ mục cho bảng `revenue_report`
--
ALTER TABLE `revenue_report`
  ADD PRIMARY KEY (`id`);

--
-- Chỉ mục cho bảng `room`
--
ALTER TABLE `room`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id_2` (`id`),
  ADD UNIQUE KEY `id_3` (`id`),
  ADD KEY `id_4` (`id`);

--
-- Chỉ mục cho bảng `room_rental_detail`
--
ALTER TABLE `room_rental_detail`
  ADD PRIMARY KEY (`id`);

--
-- Chỉ mục cho bảng `staff`
--
ALTER TABLE `staff`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `customer`
--
ALTER TABLE `customer`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT cho bảng `payment_detail`
--
ALTER TABLE `payment_detail`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT cho bảng `rent_info`
--
ALTER TABLE `rent_info`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT cho bảng `revenue_report`
--
ALTER TABLE `revenue_report`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `room`
--
ALTER TABLE `room`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT cho bảng `room_rental_detail`
--
ALTER TABLE `room_rental_detail`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT cho bảng `staff`
--
ALTER TABLE `staff`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
