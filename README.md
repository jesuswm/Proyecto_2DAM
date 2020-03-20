Proyecto_2DAM

# 12/03/2020
1. Subido el proyecto de unity
2. Se han agregado los siguientes scripts para la generación del mapa en tiempo de ejecución:
* CrearArchivo: Clase que permite guardar y cargar un mapa
* Enemigos: Clase estática que asocia el enumerado eEnemigo con el GameObject correspondiente al enemigo
* GenerarMapa: Clase que genera un mapa en la escena en función de un archivo
* Mapa: Clase que se genera a partir de una Lista de ObjetoMapa y los clasifica en funcion de su tipo.
* ObjetoMapa: Clase serializable que contiene las variables comunes de cualquier objeto del mapa
* Tiles: Clase estática que asocia el enumerado eTiles con un tile de los recursos del proyecto

# 20/03/2020
1. Restructuración de las clases: ObjetoMapa, Mapa , GenerarMapa
2. Agregadas más clases para el guardado de la información del mapa en un archivo:
* ArbustoMapa, EnemigoMapa, JugadorMapa, ObstaculosMapa, TileMapa
3. Agregado primer prototipo de editor 
4. Añadidas las siguientes clases para su funcionamiento del editor
* CreadorDeCuadriculas: Clase que gestiona el conjunto de celdas la edición en el editor.
* PulsarCuadricula: Clase que se encarga de gestionar la pulsación de una cuadricula.
* SeleccionDeHerramienta: Clase que gestiona la selección de herramientas.
* MoverCamara: Clase que gestiona el desplazamiento de la cámara en el editor.
* ActualizarImagenPincel: Clase que se encarga del cambio de la imagen (en la barra de herramientas) según el pincel seleccionado.
5. Se han añadido múltiples objetos a la carpeta Resources para el funcionamiento del editor (prefabricados de enemigos etc …).
6. Se han modificado algunos scripts antiguos para que sean funcionales con los mapas generados en tiempo de ejecución.

