BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Partidas" (
	"IdPartida"	INTEGER NOT NULL,
	"IdJugador"	INTEGER NOT NULL,
	"Intentos"	INTEGER DEFAULT 0,
	"Aciertos"	INTEGER DEFAULT 0,
	"Errores"	INTEGER DEFAULT 0,
	"Nivel"	INTEGER NOT NULL,
	"Dificultad"	INTEGER NOT NULL,
	"FechaTiempo"	TEXT NOT NULL,
	"Tiempo"	INTEGER DEFAULT 0,
	FOREIGN KEY("IdJugador") REFERENCES "Jugadores"("IdJugador"),
	PRIMARY KEY("IdPartida" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Configuraciones" (
	"IdConfiguracion"	INTEGER NOT NULL,
	"IdJugador"	INTEGER NOT NULL,
	"Volumen"	INTEGER DEFAULT 50,
	"SFX"	INTEGER DEFAULT 50,
	FOREIGN KEY("IdJugador") REFERENCES "Jugadores"("IdJugador"),
	PRIMARY KEY("IdConfiguracion" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Psicologos" (
	"IdPsicologo"	INTEGER NOT NULL,
	"Nombre"	TEXT NOT NULL,
	"Correo"	TEXT NOT NULL UNIQUE,
	"Cedula"	TEXT,
	"Telefono"	TEXT NOT NULL,
	"Contrasena"	TEXT NOT NULL,
	PRIMARY KEY("IdPsicologo" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Tutores" (
	"IdTutor"	INTEGER NOT NULL,
	"Nombre"	TEXT NOT NULL,
	"Telefono"	TEXT NOT NULL,
	"Correo"	TEXT NOT NULL UNIQUE,
	PRIMARY KEY("IdTutor" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Jugadores" (
	"IdJugador"	INTEGER NOT NULL,
	"Nombre"	TEXT NOT NULL,
	"Contrasena"	TEXT NOT NULL,
	"Edad"	INTEGER NOT NULL,
	"Sexo"	TEXT NOT NULL,
	"NombreUsuario"	TEXT NOT NULL UNIQUE,
	"IdTutor"	INTEGER NOT NULL,
	"IdPsicologo"	INTEGER NOT NULL,
	"Sesion"	INTEGER NOT NULL DEFAULT 0,
	FOREIGN KEY("IdPsicologo") REFERENCES "Psicologos"("IdPsicologo"),
	FOREIGN KEY("IdTutor") REFERENCES "Tutores"("IdTutor"),
	PRIMARY KEY("IdJugador" AUTOINCREMENT)
);
CREATE TRIGGER TR_Jugadores_BU BEFORE UPDATE 
ON Jugadores
BEGIN
	UPDATE Jugadores
	SET Sesion = 0
	WHERE NEW.Sesion = 1 
	AND IdJugador != NEW.IdJugador;
END;
COMMIT;
