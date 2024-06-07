
# GUIA DE LOS PROYECTOS

EXISTEN TRES CARPETAS CON EL CONTENIDO DE LA EVALUACION

- EvaluacionExperisNet
- ExperisEvaluacionAPI
- experis-evaluacion-react

La caperta 'EvaluacionExperisNet' contiene la solución de la parte 1 de la evaluación con los archivos solicitados 'OrderRange.cs' y 'MoneyParts.cs'.

Las carpetas 'ExperisEvaluacionAPI' y 'experis-evaluacion-react' contienen la solución de la pregunta 2.

## PASOS PARA EJECUTAR EL BACKEND ('ExperisEvaluacionAPI')

    1. INSTALAR TODOS LOS PLUGIN DEL PROYECTO DESDE EL ADMINISTRAR PAQUETES NuGet

    2. EN POSTGRES CREAR LA BASE DE DATOS 'experis_evaluacion'

	3. EJECUTAR EL COMANDO DESDE LA CONSOLA ADMINISTRADOR DE PAQUETES PARA LA CREACION DE TABLA USUARIO

		dotnet ef migrations add InitialCreate

		dotnet ef database update

	4.	EJECUTA EL COMANDO PARA CREAR EL USUARIO ADMINISTRADOR EN POSTGRES

		INSERT INTO usuarios (
			"Email", 
			"Password", 
			"FechaNacimiento", 
			"Genero", 
			"Roles", 
			"UsuarioCreador",
			"Bloquear",
			"MarcaTemporalCreacion",
			"UsuarioActualizador",
			"UsuarioEliminador",
			"EstadoEliminado"
		)
		VALUES (
			'admin@experis.com', 
			'$2b$10$2slr3.DKGJVhUWo0svfG8OAoTbNQXOV3Sk8QHaSkgtA5Mo1ohxQm2', 
			'1994-10-18', 
			'M', 
			'Administrador', 
			'admin@experis.com',
			false,
			'2024-06-06',
			'',
			'',
			false
		);
		
	5.	VERIFICAR EL REGISTRO
	
		SELECT * FROM usuarios; 

	6. EJECUTAR EL PROYECTO
	
		USUARIO: admin@experis
		PASSWORD: admin

## PASOS PARA EJECUTAR EL FRONTEND ('experis-evaluacion-react')

	1.	INGRESAR A LA CARPETA DEL PROYECTO Y INSTALAR DEPENDENCIAS CON EL COMANDO
	
		npm install
		
	2.	MODIFICAR EL ARCHIVO .ENV CON LA URL DE LA API
	
		REACT_APP_API_URL
		
	3. EJECUTAR EL PROYECTO CON EL COMANDO
	
		npm run
		
	4. UTILIZAR LAS CREDENCIALES DEL ADMINISTRADOR
	
		USUARIO: admin@experis.com
		PASSWORD: admin