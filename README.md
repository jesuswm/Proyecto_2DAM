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


# 27/03/2020

1. Cambios en el funcionamiento del editor:
	* Ahora no se pueden colocar objetos (enemigos, arbustos, obstáculos …) sobre casillas en las que no se haya asignado ningún suelo previamente.
	* Ahora cuando intentamos colocar un objeto sobre una casilla no valida se reproduce un sonido de error (modificaciones en la función pulsar() de la clase PulsarCuadricula).
2. Se ha añadido un botón en el editor que permite cargar el mapa guardado y continuar editándolo:
	* Para el funcionamiento de este botón se agregado la función CargarMapaAlEditor() en la clase CreadorDeCuadriculas
3. Se ha modificado la forma en que se visualiza la imagen del pincel seleccionado provocando la eliminación del script ActualizarImagenPincel.
4. Agregados nuevos pinceles a la herramienta suelo (se han agregado al enumerado eTiles y se han añadido sus respectivos tiles a la carpeta Resources del proyecto).
5. Se ha arreglado un error que provocaba que cuando jugaras un mapa generado con el editor el botón de ataque de la interfaz no funcionara.
	* Para solucionarlo se ha agregado la clase BotonAtacarAutoGenerado
	
# 3/04/2020

1. Cambios en la interfaz del editor:
	* Ahora la selección de herramienta se realiza mediante la gestión de un Toggle Grop en lugar de usar un Dropdown (La mayor parte de las modificaciones se han realizado en el script SeleccionDeHerramienta anqué también se han realizado cambios menores en los scripts PulsarCuadricula y MoverCamara)
	* Ahora los pinceles de la herramienta suelo ya tienen definido si son traspasables o no mediante código por lo que se ha eliminado el Toggle de traspasable de la interfaz del editor.
2. Cambios en el funcionamiento del editor:
	* Ahora no se pueden colocar objetos (enemigos, arbustos, obstáculos …) en suelos no traspasables (modificaciones en la función actualizarEvento() de la clase PulsarCuadricula)
	* Ahora cuando tenemos seleccionada una herramienta las casillas en las que no se pueda usar esa herramienta aumentaran su transparencia, por ejemplo, si tenemos seleccionada la herramienta enemigos todas las casillas en las que no podamos colocar un enemigo (Casillas asociadas a un obstáculo, suelo no traspasable o sin suelo asociado) aumentaran su transparencia (La comprobación se realiza en el Update() de PulsarCuadricula).
	* Ahora al cambiar de una herramienta a otra se almacena el pincel actual de la herramienta que se estaba usando de esta forma al volver a la herramienta anterior se mantiene en ese pincel.
3. Agregados nuevos pinceles a la herramienta obstaculos (se han agregado al enumerado eObstaculos, se han añadido sus respectivos GameObject a la carpeta Resources del proyecto y se ha actualizado las funciones bloquearEventosAlrrededor, guardarMapa y CargarMapaAlEditor de la clase CreadorDeCuadriculas para añadirlos).

# 10/04/2020

1. Se ha establecido la condición de victoria de los mapas personalizados.
	* La condición de victoria para completar un mapa personalizado es eliminar a todos los enemigos del mapa.
2. Mensaje de error al generar mapa
	* Ahora si el mapa que intentamos cargar no cumple los requisitos para ser jugable se nos mostrara un mensaje explicando que el mapa no es jugable y por lo cual debe ser editado y especificando cuales son los requisitos para que un mapa sea jugable.
	* Los requisitos para que el mapa sea jugable son los siguiente:
		* En el mapa debe establecerse la posición de inicio del jugador.
		* El mapa debe tener como mínimo un enemigo.
3. Se han modificado algunos scripts antiguos (GestionPantallasPartida, Follow) para controlar posibles excepciones que podrían suceder al intentar generar mapas no jugables.
4. Se ha añadido la traducción ha inglés de todos los textos del editor y los mensajes de error de forma que si la configuración del juego está en ingles todos los textos se mostrarán en inglés.

# 17/04/2020

1. Se ha modificado el sistema de guardado de mapas permitiéndonos que existan múltiples mapas en el dispositivo.
	* Ahora al pulsar el botón de guardar en el editor aparecerá una pantalla que nos permitirá ponerle un nombre al mapa, este nombre será único en caso de que ya exista un mapa con ese nombre este se sobrescribirá (En caso de que esto fuera ocurrir se le indicara al usuario con un mensaje de advertencia en color amarillo)
	* La pantalla también nos permite cancelar el guardado volviendo a la ventana de edición sin realizar el guardado.
	* Si durante la sesión de edición de mapa ya se ha guardado o cargado un mapa previamente por defecto al abrir el dialogo de guardar aparecerá el nombre de ese mapa, es decir si tú has guardado el mapa con un nombre “a” la próxima vez que abras el dialogo de guardar en esa misma sesión el nombre del mapa por defecto al guardar será “a”.
	* Para el funcionamiento de esta pantalla se han añadido los siguientes scripts:
		* TeclaTeclado: Clase que se encarga de gestionar el funcionamiento de las teclas del teclado que se muestra para poner nombre al mapa.
		* BotonGuardarTeclado: Clase que gestiona el botón de guardado de la pantalla de guardado
		* MensajeErrorNombreMapa: Clase que se encarga de actualizar el mensaje de error si el nombre dado al mapa no es válido o ya existe.
2. Se ha modificado el sistema de cargado de mapas en el editor
	* Ahora al pulsar el botón de cargar en el editor aparecerá una pantalla que nos permitirá seleccionar el mapa que deseamos cargar en el editor la selección del mapa se realiza mediante un dropdown que contiene todos los nombres de los mapas guardados previamente.
	* La pantalla también nos permite cancelar la acción volviendo a la ventana de edición sin realizar ningún cargado.
	* Ahora el botón de cargar solo estará habilitado si existe algún mapa guardado.
	* Para el funcionamiento de esta pantalla se han añadido los siguientes scripts:
		* PantallaCargar: Clase que se encarga de gestionar el funcionamiento de la pantalla de carga.
3. Añadido botón de borrar mapa en el edito
	* Este botón nos permite acceder una ventana en la que podremos seleccionar un mapa guardado y eliminarlo, la selección del mapa se realiza mediante un dropdown que contiene todos los nombres de los mapas guardados previamente.
	* La pantalla también nos permite cancelar la acción volviendo a la ventana de edición sin borrar ningún mapa.
	* El botón solo estará habilitado si existe algún mapa guardado.
	* Para el funcionamiento de esta pantalla se han añadido los siguientes scripts:
		* PantallaBorrado: Clase que se encarga de gestionar el funcionamiento de la pantalla de borrado.
		
# 24/04/2020

1. Se ha agregado a la ventana de error al generar mapa un nuevo botón de “editar” al pulsarlo se abrirá el editor de mapas con el mapa en cuestión listo para ser editado.
2. Se han arreglado múltiples errores:
	* Arreglado un error en el teclado mostrado para ponerle un nombre al mapa.
	* Arreglado un error por el cual si abríamos una de las nuevas pantallas agregadas al editor en la última actualización (cargar mapa, guardar mapa y borrar mapa) mientras la herramienta actual en el editor era MoverCamara se seguía permitiendo mover y hacer zoom cuando no se debería.
	* Arreglado un error de colisiones al generar el mapa que podía derivar a la colocación del jugador en zonas no traspasables.
3. Se ha añadido la traducción ha inglés de las nuevas pantallas agregadas en la anterior actualización (cargar mapa, guardar mapa y borrar mapa) de forma que si la configuración del juego está en ingles todos los textos se mostrarán en inglés.