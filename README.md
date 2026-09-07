# Sistema de Gestión de Hacienda Ganadera - Fase 2 (Patrones de Diseño)

Proyecto académico de Arquitectura de Software — Segunda evolución de un sistema de gestión ganadera (potreros, reses, vacunas, ventas, usuarios). En esta fase, transicionamos de un diseño basado puramente en principios SOLID hacia una arquitectura **robusta y extensible** mediante la aplicación de Patrones de Diseño, reduciendo rigideces estructurales sin alterar el comportamiento observable del negocio.

>  **Todo el trabajo del proyecto (código fuente, diagramas y documentación) se encuentra en la rama `development`, no en `main`.** Asegúrate de cambiar a esa rama después de clonar el repositorio (ver paso 1 más abajo).

> **Link del video de youtube: https://www.youtube.com/watch?v=Y_3A9b2mBMg**

**Nota:** Si la calidad del video al inicio es baja, por favor ajusta la configuración del reproductor de YouTube a la calidad más alta (1080p HD). En Youtube: Configuracion -> Calidad -> 10809 HD

## Estructura del proyecto

```
proyecto_hacienda/
├── Bib_Hacienda/          # Biblioteca de clases con la lógica de Dominio
├── p_mvcHacienda/         # Proyecto ASP.NET Core MVC (Presentación + Aplicación + Infraestructura)
├── documentos/            # Diagramas UML (AS-IS y TO-BE)
└── proyecto_hacienda.slnx # Archivo de solución (.NET, formato nuevo)
```

## Requisitos previos

- .NET SDK 9 instalado.
- (Opcional) Rider, Visual Studio o VS Code con la extensión de C#, si prefieres abrir y ejecutar desde un IDE en lugar de la terminal.

## Instrucciones de ejecución paso a paso

### 1. Clonar el repositorio y cambiar a la rama `development`

```bash
git clone https://github.com/Diego018/proyecto_hacienda.git
cd proyecto_hacienda
git checkout development
```

### 2. Restaurar dependencias y compilar toda la solución

Parado en la raíz del repositorio (donde está `proyecto_hacienda.slnx`):

```bash
dotnet build proyecto_hacienda.slnx
```

Esto compila ambos proyectos (`Bib_Hacienda` y `p_mvcHacienda`) y restaura automáticamente los paquetes NuGet necesarios.

### 3. Ejecutar el proyecto principal

```bash
cd p_mvcHacienda
dotnet run
```

Al iniciar, la consola ejecuta automáticamente nuestro Motor de Aserción Automática (PruebasCaracterizacion.cs). Este audita en tiempo real que las salidas de la arquitectura refactorizada (TO-BE) coincidan exactamente con la línea base original (AS-IS), certificando que el comportamiento del sistema se preservó.

### 4. Acceder a la aplicación

Una vez levantado el servidor, la consola mostrará una línea similar a:

```
Now listening on: http://localhost:XXXX
```

Abre esa URL en tu navegador (el puerto puede variar según tu máquina).

### 5. Detener la aplicación

Desde la terminal donde se está ejecutando:

```
Ctrl + C
```

## Persistencia de datos

El sistema guarda su información en archivos de texto plano dentro de la carpeta `p_mvcHacienda/Datos/` (`Hacienda.txt`, `Ventas.txt`, `Usuarios.txt`), gestionados por `PersistenciaTxtService`. No requiere ninguna base de datos externa.

---

## Identificación del equipo y roles de trabajo

| Integrante | ID | Rol Asignado | Frente de Responsabilidad | % Part. |
|---|---|---|---|---|
| Diego Villegas | 000553180 | Arquitecto Líder | Detección de puntos rígidos, selección/descarte de patrones, diseño TO-BE. | 100% |
| Salomé Fonseca | 000248599 | Arquitecta de Riesgos | Análisis de riesgo, evaluación de exposición y plan de mitigación técnica. | 100% |
| María Fernanda Muñoz | 000543839 | Ingeniero de Comportamiento | Pruebas de caracterización, evidencia de que la conducta observable se preservó, escenarios de ejecución del programa principal. | 100% |
| Pilar Mantilla | 000547259 | Integrador y Evidencia | Construcción de vista de negocio, guía técnica para desarrolladores, bitácora IA y consolidación del documento. | 100% |
