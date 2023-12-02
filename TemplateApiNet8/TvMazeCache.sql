BEGIN TRANSACTION;
DROP TABLE IF EXISTS "Show";
CREATE TABLE IF NOT EXISTS "Show" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Url"	TEXT,
	"Name"	TEXT,
	"Runtime"	INTEGER,
	"AverageRuntime"	INTEGER,
	"Premiered"	TEXT,
	"Ended"	TEXT,
	"OfficialSite"	TEXT,
	"Weight"	INTEGER,
	"Summary"	TEXT,
	"Updated"	INTEGER,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "Kind";
CREATE TABLE IF NOT EXISTS "Kind" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Status";
CREATE TABLE IF NOT EXISTS "Status" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Language";
CREATE TABLE IF NOT EXISTS "Language" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Network";
CREATE TABLE IF NOT EXISTS "Network" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT,
	"OfficialSite"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Country";
CREATE TABLE IF NOT EXISTS "Country" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT,
	"Code"	TEXT,
	"Timezone"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Externals";
CREATE TABLE IF NOT EXISTS "Externals" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Image";
CREATE TABLE IF NOT EXISTS "Image" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"ShowId"	INTEGER NOT NULL,
	"Medium"	TEXT,
	"Original"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Episode";
CREATE TABLE IF NOT EXISTS "Episode" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"ShowId"	INTEGER NOT NULL,
	"Href"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Links";
CREATE TABLE IF NOT EXISTS "Links" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"ShowId"	INTEGER NOT NULL,
	"SelfId"	INTEGER,
	"PreviousId"	INTEGER,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("SelfId") REFERENCES "Episode"("Id"),
	FOREIGN KEY("PreviousId") REFERENCES "Episode"("Id")
);
DROP TABLE IF EXISTS "Genere";
CREATE TABLE IF NOT EXISTS "Genere" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Schedule";
CREATE TABLE IF NOT EXISTS "Schedule" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"ShowId"	INTEGER NOT NULL,
	"Time"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id")
);
DROP TABLE IF EXISTS "Day";
CREATE TABLE IF NOT EXISTS "Day" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "Rating";
CREATE TABLE IF NOT EXISTS "Rating" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"ShowId"	INTEGER NOT NULL,
	"Average"	REAL NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id")
);
DROP TABLE IF EXISTS "ShowExternals";
CREATE TABLE IF NOT EXISTS "Externals" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"ShowId"	INTEGER NOT NULL,
	"ExternalId"	INTEGER NOT NULL,
	"Value"	REAL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
DROP TABLE IF EXISTS "ScheduleDay";
CREATE TABLE IF NOT EXISTS "ScheduleDay" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"ScheduleId"	INTEGER NOT NULL,
	"DayId"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("ScheduleId") REFERENCES "Schedule"("Id"),
	FOREIGN KEY("DayId") REFERENCES "Day"("Id")
);
DROP TABLE IF EXISTS "ShowGenere";
CREATE TABLE IF NOT EXISTS "ShowGenere" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"ShowId"	INTEGER NOT NULL,
	"GenereId"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id"),
	FOREIGN KEY("GenereId") REFERENCES "Genere"("Id")
);
DROP TABLE IF EXISTS "ShowNetwork";
CREATE TABLE IF NOT EXISTS "ShowNetwork" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"ShowId"	INTEGER NOT NULL,
	"NetworkId"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id"),
	FOREIGN KEY("NetworkId") REFERENCES "Network"("Id")
);
DROP TABLE IF EXISTS "CountryNetwork";
CREATE TABLE IF NOT EXISTS "CountryNetwork" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"NetworkId"	INTEGER NOT NULL,
	"CountryId"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("NetworkId") REFERENCES "Network"("Id"),
	FOREIGN KEY("CountryId") REFERENCES "Country"("Id")
);
DROP TABLE IF EXISTS "ShowLanguage";
CREATE TABLE IF NOT EXISTS "ShowLanguage" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"ShowId"	INTEGER NOT NULL,
	"LanguageId"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id"),
	FOREIGN KEY("LanguageId") REFERENCES "Language"("Id")
);
DROP TABLE IF EXISTS "ShowKind";
CREATE TABLE IF NOT EXISTS "ShowKind" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"ShowId"	INTEGER NOT NULL,
	"KindId"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id"),
	FOREIGN KEY("KindId") REFERENCES "Kind"("Id")
);
DROP TABLE IF EXISTS "ShowStatus";
CREATE TABLE IF NOT EXISTS "ShowStatus" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"ShowId"	INTEGER NOT NULL,
	"StatusId"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id"),
	FOREIGN KEY("StatusId") REFERENCES "Status"("Id")
);
COMMIT;
