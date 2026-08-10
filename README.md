# Sistema de Gestión de Hacienda Ganadera

Proyecto académico de Arquitectura de Software — refactorización de un sistema de gestión ganadera (potreros, reses, vacunas, ventas, usuarios) aplicando los 5 principios SOLID, siguiendo una arquitectura en capas (Presentación, Aplicación, Dominio, Infraestructura).

>  **Todo el trabajo del proyecto (código fuente, diagramas y documentación) se encuentra en la rama `development`, no en `main`.** Asegúrate de cambiar a esa rama después de clonar el repositorio (ver paso 1 más abajo).
> **Link del video de youtube: https://youtu.be/dy-CJhaxa8I **

## Estructura del proyecto

```
proyecto_hacienda/
├── Bib_Hacienda/          # Biblioteca de clases con la lógica de Dominio
├── p_mvcHacienda/         # Proyecto ASP.NET Core MVC (Presentación + Aplicación + Infraestructura)
├── documentos/            # Diagramas UML (AS-IS y TO-BE)
└── proyecto_hacienda.slnx # Archivo de solución (.NET, formato nuevo)
```

## Requisitos previos

- .NET SDK 10 instalado.
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

Al iniciar, la consola imprime automáticamente los 8 casos de caracterización (`PruebasCaracterizacion.cs`), como evidencia de que el comportamiento del sistema se preservó tras la refactorización (AS-IS vs TO-BE).

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
| Diego Villegas | 000553180 | Arquitecto de Dominio | Identificación de responsabilidades y límites de cada clase (SRP), modelo del dominio, jerarquías de herencia y su validez frente a LSP. | 100% |
| Salomé Fonseca | 000248599 | Arquitecto de Dependencias | Mapa de dependencias, abstracciones (interfaces), inversión e inyección de dependencias, composition root (DIP, ISP). | 100% |
| María Fernanda Muñoz | 000543839 | Ingeniero de Comportamiento | Pruebas de caracterización, evidencia de que la conducta observable se preservó, escenarios de ejecución del programa principal. | 100% |
| Pilar Mantilla | 000547259 | Integrador y Evidencia | Consistencia diagrama–código, estructura del entregable, bitácora de uso de IA, métricas antes/después. | 100% |
