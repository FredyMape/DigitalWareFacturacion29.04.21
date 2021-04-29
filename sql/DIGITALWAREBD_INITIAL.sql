GO
CREATE DATABASE [DIGITALWARE_DB]

GO
USE [DIGITALWARE_DB]

/*TABLAS SIN CRUD*/
GO
CREATE TABLE CATEGORIA (
	PK_Id_categoria			BIGINT			PRIMARY KEY		IDENTITY (1,1),
	nombre					NVARCHAR(50)	NOT NULL,
	descripcion				NVARCHAR(150)	NOT NULL,
	
	/*AUDITORIA*/
	fechaRegistro			DATETIME		NOT NULL,
	fechaActualizacion		DATETIME		NULL,
	ultimoModificador		BIGINT			NOT NULL,
);

CREATE TABLE TIPO_PAGO (
	PK_Id_tipoPago			BIGINT			PRIMARY KEY		IDENTITY (1,1),
	nombre					NVARCHAR(50)	NOT NULL,
	descripcion				NVARCHAR(150)	NOT NULL,

	/*AUDITORIA*/
	fechaRegistro			DATETIME		NOT NULL,
	fechaActualizacion		DATETIME		NULL,
	ultimoModificador		BIGINT			NOT NULL,
);

CREATE TABLE CLIENTE (
	PK_Id_cliente			BIGINT			PRIMARY KEY		IDENTITY (1,1),
	nombre					NVARCHAR(150)	NOT NULL,
	apellido				NVARCHAR(150)	NOT NULL,
	direccion				NVARCHAR(30)	NOT NULL,
	fechaNacimineto			DATE			NOT NULL,
	telefono				NVARCHAR(30)	NOT NULL,
	email					NVARCHAR(150)	NOT NULL,

	/*AUDITORIA*/
	fechaRegistro			DATETIME		NOT NULL,
	fechaActualizacion		DATETIME		NULL,
	ultimoModificador		BIGINT			NOT NULL
);

CREATE TABLE PRODUCTO (
	PK_Id_producto			BIGINT			PRIMARY KEY		IDENTITY (1,1),
	nombre					NVARCHAR(150)	NOT NULL,
	precio					MONEY			NOT NULL,
	stock					TINYINT	NOT NULL,

	/*AUDITORIA*/
	fechaRegistro			DATETIME		NOT NULL,
	fechaActualizacion		DATETIME		NULL,
	ultimoModificador		BIGINT			NOT NULL,

	/*FOREIGN KEY*/
	FK_Id_categoria			BIGINT			NOT NULL
);


CREATE TABLE FACTURA (
	PK_Id_factura			BIGINT			PRIMARY KEY		IDENTITY (1,1),
	
	/*AUDITORIA*/
	fechaRegistro			DATETIME		NOT NULL,
	ultimoModificador		BIGINT			NOT NULL,

	/*FOREIGN KEY*/
	FK_Id_cliente			BIGINT			NOT NULL,
	FK_Id_tipoPago			BIGINT			NOT NULL
);

CREATE TABLE DETALLE_FACTURA (
	PK_Id_detalleFactura	BIGINT			PRIMARY KEY		IDENTITY (1,1),
	cantidad				TINYINT			NOT NULL,
	precio					MONEY			NOT NULL,

	/*AUDITORIA*/
	fechaRegistro			DATETIME		NOT NULL,
	ultimoModificador		BIGINT			NOT NULL,

	/*FOREIGN KEY*/
	FK_Id_factura			BIGINT			NOT NULL,
	FK_Id_producto			BIGINT			NOT NULL
);

/*FOREIGN KEYS*/
ALTER TABLE PRODUCTO			ADD CONSTRAINT	FK_01_PRODUCTO_CATEGORIA			FOREIGN KEY (FK_Id_categoria)	REFERENCES	CATEGORIA		(PK_Id_categoria);

ALTER TABLE FACTURA				ADD	CONSTRAINT	FK_02_FACTURA_CLIENTE				FOREIGN KEY (FK_Id_cliente)		REFERENCES	CLIENTE			(PK_Id_cliente);
ALTER TABLE FACTURA				ADD	CONSTRAINT	FK_03_FACTURA_TIPO_PAGO				FOREIGN KEY (FK_Id_tipoPago)	REFERENCES	TIPO_PAGO		(PK_Id_tipoPago);

ALTER TABLE DETALLE_FACTURA		ADD	CONSTRAINT	FK_04_DETALLE_FACTURA_FACTURA		FOREIGN KEY (FK_Id_factura)		REFERENCES	FACTURA			(PK_Id_factura);
ALTER TABLE DETALLE_FACTURA		ADD	CONSTRAINT	FK_05_DETALLE_FACTURA_PRODUCTO		FOREIGN KEY (FK_Id_producto)	REFERENCES	PRODUCTO		(PK_Id_producto);





INSERT INTO CLIENTE VALUES('David Stiven', 'Mape Sanabria', 'Cra 160B #132D-23', '1998-02-10', '3057102133', 'dsms10@gmail.com', GETDATE(), null, 1);
INSERT INTO CLIENTE VALUES('Nevada Viviana', 'Meneces Fern�ndez', 'Calle Santo del Cascado No 764', '1981-03-24', '+593(252)-8338656', 'djgladisf9@yopmail.com', GETDATE(), null, 1);
INSERT INTO CLIENTE VALUES('Abigail F.', 'Cepedal Crujera', 'Cerrada Jonathan No. 385', '1965-11-06', '+57(252)-9666885', 'cbcrujera1@yopmail.com', GETDATE(), null, 1);
INSERT INTO CLIENTE VALUES('Luisa Z. ', 'Love Bouazama', 'Avenida Santillan No. 773', '1971-06-20', '+52(333)-0114555', 'ahluisaz7@yopmail.com', GETDATE(), null, 1);
INSERT INTO CLIENTE VALUES('Arquimedes ', 'Buenadicha Cornes', 'Bulevar Cuscatlan No. 257', '1978-11-20', '+1-787(353)-8660685', 'gfcornes5@yopmail.com', GETDATE(), null, 1);
INSERT INTO CLIENTE VALUES('Joseba ', 'Jovanovic Jaouad', 'Calle Santo del Julio No. 948', '1984-11-19', '+1-787(151)-6996823', 'dqjaouad16@yopmail.com', GETDATE(), null, 1);
INSERT INTO CLIENTE VALUES('Salvador', 'Shevchuk De Pascual', 'Boulevard Romana No. 299', '1991-10-9', '+1-787(232)-7664515', 'ikdepascual10@yopmail.com', GETDATE(), null, 1);


INSERT INTO TIPO_PAGO VALUES('Contado', 'El cliente paga la totalidad del producto en efectivo', GETDATE(), null, 1);
INSERT INTO TIPO_PAGO VALUES('Credito', 'El cliente paga la totalidad del producto con una tarjeta de credito', GETDATE(), null, 1);
INSERT INTO TIPO_PAGO VALUES('Debito', 'El cliente paga la totalidad del producto con una tarjeta dedebito', GETDATE(), null, 1);


INSERT INTO CATEGORIA VALUES('Comida','productos comenstibles',GETDATE(), null, 1);
INSERT INTO CATEGORIA VALUES('Bebidas','productos liquidos',GETDATE(), null, 1);
INSERT INTO CATEGORIA VALUES('Postres','productos de pasteleria',GETDATE(), null, 1);


INSERT INTO PRODUCTO VALUES('Galletas wafer', 800, '10', GETDATE(), null, 1, 1);
INSERT INTO PRODUCTO VALUES('Chocorramo', 2600, '59', GETDATE(), null, 1, 1);
INSERT INTO PRODUCTO VALUES('Doritos', 7000, '32', GETDATE(), null, 1, 1);
INSERT INTO PRODUCTO VALUES('Ponimalta', 5300, '45', GETDATE(), null, 1, 1);
INSERT INTO PRODUCTO VALUES('Colombiana', 4600, '35', GETDATE(), null, 1, 1);
INSERT INTO PRODUCTO VALUES('Cuatro', 3400, '85', GETDATE(), null, 1, 1);
INSERT INTO PRODUCTO VALUES('Postre de leche', 2000, '7', GETDATE(), null, 1, 1);
INSERT INTO PRODUCTO VALUES('Torta de cumplea�os', 18500, '32', GETDATE(), null, 1, 1);
INSERT INTO PRODUCTO VALUES('Flan', 3500, '61', GETDATE(), null, 1, 1);


INSERT INTO FACTURA VALUES(GETDATE(), 1, 3, 1);
INSERT INTO FACTURA VALUES(GETDATE(), 1, 4, 2);
INSERT INTO FACTURA VALUES(GETDATE(), 1, 6, 3);
INSERT INTO FACTURA VALUES(GETDATE(), 1, 6, 1);
INSERT INTO FACTURA VALUES(GETDATE(), 1, 7, 2);
INSERT INTO FACTURA VALUES(GETDATE(), 1, 8, 3);
INSERT INTO FACTURA VALUES(GETDATE(), 1, 9, 1);
INSERT INTO FACTURA VALUES(GETDATE(), 1, 10, 2);
INSERT INTO FACTURA VALUES(GETDATE(), 1, 11, 3);
INSERT INTO FACTURA VALUES(GETDATE(), 1, 12, 1);


INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 1, 1);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 2, 2);
INSERT INTO DETALLE_FACTURA VALUES(6, 5000, GETDATE(), 1, 3, 3);
INSERT INTO DETALLE_FACTURA VALUES(8, 5000, GETDATE(), 1, 4, 1);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 5, 2);
INSERT INTO DETALLE_FACTURA VALUES(4, 5000, GETDATE(), 1, 6, 3);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 7, 1);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 8, 2);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 9, 3);
INSERT INTO DETALLE_FACTURA VALUES(5, 5000, GETDATE(), 1, 10, 1);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 1, 2);
INSERT INTO DETALLE_FACTURA VALUES(5, 5000, GETDATE(), 1, 2, 3);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 3, 1);
INSERT INTO DETALLE_FACTURA VALUES(9, 5000, GETDATE(), 1, 4, 2);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 5, 3);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 6, 1);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 7, 2);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 8, 3);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 9, 1);
INSERT INTO DETALLE_FACTURA VALUES(6, 5000, GETDATE(), 1, 10, 2);
INSERT INTO DETALLE_FACTURA VALUES(5, 5000, GETDATE(), 1, 1, 3);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 2, 1);
INSERT INTO DETALLE_FACTURA VALUES(7, 5000, GETDATE(), 1, 3, 2);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 4, 3);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 5, 1);
INSERT INTO DETALLE_FACTURA VALUES(5, 5000, GETDATE(), 1, 6, 2);
INSERT INTO DETALLE_FACTURA VALUES(4, 5000, GETDATE(), 1, 7, 3);
INSERT INTO DETALLE_FACTURA VALUES(4, 5000, GETDATE(), 1, 8, 1);
INSERT INTO DETALLE_FACTURA VALUES(4, 5000, GETDATE(), 1, 9, 2);
INSERT INTO DETALLE_FACTURA VALUES(6, 5000, GETDATE(), 1, 10, 3);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 1, 1);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 2, 2);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 3, 3);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 1, 1);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 2, 2);
INSERT INTO DETALLE_FACTURA VALUES(6, 5000, GETDATE(), 1, 3, 3);
INSERT INTO DETALLE_FACTURA VALUES(8, 5000, GETDATE(), 1, 4, 4);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 5, 5);
INSERT INTO DETALLE_FACTURA VALUES(4, 5000, GETDATE(), 1, 6, 6);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 7, 7);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 8, 8);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 9, 9);
INSERT INTO DETALLE_FACTURA VALUES(5, 5000, GETDATE(), 1, 10, 10);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 1, 11);
INSERT INTO DETALLE_FACTURA VALUES(5, 5000, GETDATE(), 1, 2, 1);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 3, 2);
INSERT INTO DETALLE_FACTURA VALUES(9, 5000, GETDATE(), 1, 4, 3);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 5,4);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 6, 5);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 7, 6);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 8, 7);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 9, 8);
INSERT INTO DETALLE_FACTURA VALUES(6, 5000, GETDATE(), 1, 10, 9);
INSERT INTO DETALLE_FACTURA VALUES(5, 5000, GETDATE(), 1, 1, 10);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 2, 11);
INSERT INTO DETALLE_FACTURA VALUES(7, 5000, GETDATE(), 1, 3, 2);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 4, 3);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 5, 4);
INSERT INTO DETALLE_FACTURA VALUES(5, 5000, GETDATE(), 1, 6, 5);
INSERT INTO DETALLE_FACTURA VALUES(4, 5000, GETDATE(), 1, 7, 6);
INSERT INTO DETALLE_FACTURA VALUES(4, 5000, GETDATE(), 1, 8, 7);
INSERT INTO DETALLE_FACTURA VALUES(4, 5000, GETDATE(), 1, 9, 8);
INSERT INTO DETALLE_FACTURA VALUES(6, 5000, GETDATE(), 1, 10, 9);
INSERT INTO DETALLE_FACTURA VALUES(2, 5000, GETDATE(), 1, 1, 10);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 2, 11);
INSERT INTO DETALLE_FACTURA VALUES(1, 5000, GETDATE(), 1, 3, 3);


SELECT * FROM CATEGORIA
SELECT * FROM PRODUCTO
SELECT * FROM CLIENTE
SELECT * FROM TIPO_PAGO
SELECT * FROM PRODUCTO
SELECT * FROM FACTURA
SELECT * FROM DETALLE_FACTURA