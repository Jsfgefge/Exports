CREATE TABLE "PolizasCoexpo"(
    "id" BIGINT NOT NULL,
    "polizaNo" BIGINT NOT NULL,
    "countryID" BIGINT NOT NULL,
    "amount" DECIMAL(8, 2) NOT NULL,
    "coexpoID" BIGINT NOT NULL
);
ALTER TABLE
    "PolizasCoexpo" ADD CONSTRAINT "polizascoexpo_id_primary" PRIMARY KEY("id");
CREATE UNIQUE INDEX "polizascoexpo_polizano_unique" ON
    "PolizasCoexpo"("polizaNo");
CREATE TABLE "Aduanas"(
    "aduanasID" BIGINT NOT NULL,
    "nombreAduana" NVARCHAR(255) NOT NULL,
    "abreviacionAduana" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Aduanas" ADD CONSTRAINT "aduanas_aduanasid_primary" PRIMARY KEY("aduanasID");
CREATE TABLE "ExportHeader"(
    "headerid" BIGINT NOT NULL,
    "invoiceNo" BIGINT NOT NULL,
    "invoiceDate" DATE NOT NULL,
    "serialNo" VARCHAR(255) NULL,
    "consignatarioID" BIGINT NOT NULL,
    "aduanasID" BIGINT NOT NULL,
    "countryID" BIGINT NOT NULL,
    "cargadorID" BIGINT NOT NULL,
    "boardingDate" DATE NOT NULL,
    "exchangeRate" DECIMAL(8, 2) NOT NULL,
    "description" NVARCHAR(255) NOT NULL,
    "docTypeID" BIGINT NOT NULL,
    "duaSimplificada" INT NULL,
    "complementaria" NVARCHAR(255) NULL,
    "incotermID" INT NOT NULL,
    "closed" BIT NOT NULL,
    "handlerID" BIGINT NULL,
    "supervisorID" BIGINT NULL,
    "anulledDate" DATETIME NULL,
    "primaImportada" BIT NOT NULL
);
ALTER TABLE
    "ExportHeader" ADD CONSTRAINT "exportheader_headerid_primary" PRIMARY KEY("headerid");
CREATE UNIQUE INDEX "exportheader_invoiceno_unique" ON
    "ExportHeader"("invoiceNo");
CREATE TABLE "Country"(
    "countryID" BIGINT NOT NULL,
    "countryName" NVARCHAR(255) NOT NULL,
    "countryISO" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Country" ADD CONSTRAINT "country_countryid_primary" PRIMARY KEY("countryID");
CREATE TABLE "PolizaImportacion"(
    "polizaID" BIGINT NOT NULL,
    "invoiceNo" BIGINT NOT NULL,
    "polizaNo" BIGINT NOT NULL,
    "countryID" BIGINT NOT NULL,
    "quantity" DECIMAL(8, 2) NOT NULL,
    "amount" DECIMAL(8, 2) NOT NULL,
    "imp" BIGINT NOT NULL,
    "linea" BIGINT NOT NULL,
    "date" DATETIME NOT NULL
);
ALTER TABLE
    "PolizaImportacion" ADD CONSTRAINT "polizaimportacion_polizaid_primary" PRIMARY KEY("polizaID");
CREATE TABLE "Consignatarios"(
    "consignatarioID" BIGINT NOT NULL,
    "nombreConsignatario" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Consignatarios" ADD CONSTRAINT "consignatarios_consignatarioid_primary" PRIMARY KEY("consignatarioID");
CREATE TABLE "Cargador"(
    "cargadorID" BIGINT NOT NULL,
    "descripcion" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Cargador" ADD CONSTRAINT "cargador_cargadorid_primary" PRIMARY KEY("cargadorID");
CREATE TABLE "FacturaCoexpo"(
    "coexpoID" BIGINT NOT NULL,
    "invoiceNo" BIGINT NOT NULL,
    "proveedor" NVARCHAR(255) NOT NULL,
    "tipoProveedor" BIGINT NOT NULL,
    "factura" NVARCHAR(255) NOT NULL,
    "amount" DECIMAL(8, 2) NOT NULL
);
ALTER TABLE
    "FacturaCoexpo" ADD CONSTRAINT "facturacoexpo_coexpoid_primary" PRIMARY KEY("coexpoID");
CREATE UNIQUE INDEX "facturacoexpo_factura_unique" ON
    "FacturaCoexpo"("factura");
CREATE TABLE "DetalleProducto"(
    "productoID" BIGINT NOT NULL,
    "invoiceNo" BIGINT NOT NULL,
    "codigoHTS" NVARCHAR(255) NOT NULL,
    "description" NVARCHAR(255) NOT NULL,
    "categoria" NVARCHAR(255) NOT NULL,
    "cantidad" BIGINT NOT NULL,
    "medida" NVARCHAR(255) NOT NULL,
    "pricePerUnit" DECIMAL(8, 2) NOT NULL
);
ALTER TABLE
    "DetalleProducto" ADD CONSTRAINT "detalleproducto_productoid_primary" PRIMARY KEY("productoID");
ALTER TABLE
    "PolizasCoexpo" ADD CONSTRAINT "polizascoexpo_coexpoid_foreign" FOREIGN KEY("coexpoID") REFERENCES "FacturaCoexpo"("coexpoID");
ALTER TABLE
    "ExportHeader" ADD CONSTRAINT "exportheader_aduanasid_foreign" FOREIGN KEY("aduanasID") REFERENCES "Aduanas"("aduanasID");
ALTER TABLE
    "PolizaImportacion" ADD CONSTRAINT "polizaimportacion_countryid_foreign" FOREIGN KEY("countryID") REFERENCES "Country"("countryID");
ALTER TABLE
    "ExportHeader" ADD CONSTRAINT "exportheader_cargadorid_foreign" FOREIGN KEY("cargadorID") REFERENCES "Cargador"("cargadorID");
ALTER TABLE
    "PolizaImportacion" ADD CONSTRAINT "polizaimportacion_invoiceno_foreign" FOREIGN KEY("invoiceNo") REFERENCES "ExportHeader"("invoiceNo");
ALTER TABLE
    "FacturaCoexpo" ADD CONSTRAINT "facturacoexpo_invoiceno_foreign" FOREIGN KEY("invoiceNo") REFERENCES "ExportHeader"("invoiceNo");
ALTER TABLE
    "DetalleProducto" ADD CONSTRAINT "detalleproducto_invoiceno_foreign" FOREIGN KEY("invoiceNo") REFERENCES "ExportHeader"("invoiceNo");
ALTER TABLE
    "ExportHeader" ADD CONSTRAINT "exportheader_consignatarioid_foreign" FOREIGN KEY("consignatarioID") REFERENCES "Consignatarios"("consignatarioID");
ALTER TABLE
    "PolizasCoexpo" ADD CONSTRAINT "polizascoexpo_countryid_foreign" FOREIGN KEY("countryID") REFERENCES "Country"("countryID");
ALTER TABLE
    "ExportHeader" ADD CONSTRAINT "exportheader_countryid_foreign" FOREIGN KEY("countryID") REFERENCES "Country"("countryID");