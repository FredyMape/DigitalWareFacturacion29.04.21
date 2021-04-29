/* LISTA DE PRECIOS DE TODOS LOS PRODUCTOS */
SELECT 
	nombre AS [Nombre_producto],
	precio AS [Precio_producto] 
	
FROM PRODUCTO
ORDER BY nombre

/* LISTA DE PRODUCTOS CUYO STOCK ESTA EN 5 UNIDADES O MENOS*/
SELECT 
	PK_Id_producto AS [Codigo_producto],
	nombre AS [Nombre_producto],
	precio AS [Precio_producto],
	stock AS [stock_producto],
	fechaActualizacion AS [ulima_actualizacion_producto]
	
FROM PRODUCTO
	WHERE stock <= 5
ORDER BY nombre


/* LISTA DE CLIENTES NO MAYORES A 35 AÑOS CON COMPRAS ENTRE EL 1 de febrero de 2000 y el 25 de mayo de 2000 */
SELECT 
	A.nombre AS [Nombres_cliente],
	A.apellido AS [Apellidos_cliente],
	A.direccion AS [Direccion_cliente],
	A.telefono AS [Telefono_cliente],
	A.nombre AS [Nombre_cliente],
	A.email AS [Email_cliente],
	CONVERT(int,ROUND(DATEDIFF(hour,fechaNacimineto,GETDATE())/8766.0,0)) AS [Edad_cliente]

FROM CLIENTE A
	INNER JOIN FACTURA B ON A.PK_Id_cliente = B.FK_Id_cliente AND B.fechaRegistro BETWEEN '2000-02-01' AND '2000-05-25'

	WHERE CONVERT(int,ROUND(DATEDIFF(HOUR, A.fechaNacimineto,GETDATE())/8766.0,0)) < 40
ORDER BY nombre 

/* VALOR TOTAL VENDIDO POR CADA PRODUCTO EN EL AÑO 2000*/
WITH TOTAL_PRODUCTOS AS(
		SELECT 
			SUM(cantidad) AS [Cantidad_producto], 
			FK_Id_producto 
	
		FROM DETALLE_FACTURA
		WHERE fechaRegistro BETWEEN '2021-02-01' AND '2021-05-25'
		GROUP BY FK_Id_producto
)

SELECT 
	A.nombre AS [Nombre_producto], 
	(B.Cantidad_producto * A.precio) AS[Total_ventas_ano]

FROM PRODUCTO A 
	INNER JOIN TOTAL_PRODUCTOS B ON A.PK_Id_producto = B.FK_Id_producto
