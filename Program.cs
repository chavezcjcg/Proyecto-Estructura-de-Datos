using System;
using System.IO; 

namespace ProyectoBiblioteca
{
    public class Libro // Clase que representa un libro en la biblioteca
    {
        public string Codigo { get; private set; }
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public string Categoria { get; private set; }
        public int copiasdisponibles { get; private set; }
        public int VecesPrestado { get; private set; }

        public Libro(string codigo, string titulo, string autor, string categoria, int copiasTotales)
        {
            Codigo = codigo;
            Titulo = titulo;
            Autor = autor;
            Categoria = categoria;
            copiasdisponibles = copiasTotales;
            VecesPrestado = 0; 
        }

        public bool Prestar() // Metodo para prestar un libro
        {
            if (copiasdisponibles <= 0) 
            {
                Console.WriteLine("No hay copias disponibles");
                return false;
            }
            else
            {
                copiasdisponibles--;
                VecesPrestado++;
                return true;
            }
        }

        public void Devolver() // Metodo para devolver un libro
        {
            copiasdisponibles++;
        }
    }


    public class MaxHeap // Clase que representa un heap maximo para los libros mas prestados
    {
        private Libro[] _heap;
        private int _tamano;

        public MaxHeap(int capacidad = 10)
        {
            _heap = new Libro[capacidad];
            _tamano = 0;
        }

        public void Insertar(Libro libro)
        {
            if (_tamano == _heap.Length) 
            {
                Redimensionar(); 
            }
            
            _heap[_tamano] = libro;
            HeapifyUp(_tamano);
            _tamano++;
        }

        public Libro ExtraerMaximo() // Metodo para extraer el libro mas prestado
        {
            if (_tamano == 0) 
            {
                Console.WriteLine("El heap esta vacio");
                return null;
            }
            else
            {
                Libro max = _heap[0]; 
                
                _tamano--; 
                _heap[0] = _heap[_tamano]; 
                
                HeapifyDown(0); 
                return max;
            }
        }

        private void HeapifyUp(int i) // Metodo para mantener la propiedad del heap al insertar un nuevo libro
        {
            while (true)
            {
                if (i > 0)
                {
                    int padre = (i - 1) / 2; 
                    
                    if (_heap[i].VecesPrestado > _heap[padre].VecesPrestado)
                    {
                        Intercambiar(i, padre);
                        i = padre; 
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void HeapifyDown(int i) // Metodo para mantener la propiedad del heap al extraer el libro mas prestado
        {
            while (true)
            {
                int mayor = i;
                int izq = 2 * i + 1; 
                int der = 2 * i + 2; 
                
                if (izq < _tamano)
                {
                    if (_heap[izq].VecesPrestado > _heap[mayor].VecesPrestado)
                    {
                        mayor = izq;
                    }
                }

                if (der < _tamano)
                {
                    if (_heap[der].VecesPrestado > _heap[mayor].VecesPrestado)
                    {
                        mayor = der;
                    }
                }

                if (mayor == i) 
                {
                    break;
                }
                else
                {
                    Intercambiar(i, mayor);
                    i = mayor;
                }
            }
        }

        private void Redimensionar()
        {
            Libro[] nuevo = new Libro[_heap.Length * 2];
            for (int i = 0; i < _tamano; i++) 
            {
                nuevo[i] = _heap[i];
            }
            _heap = nuevo;
        }

        private void Intercambiar(int i, int j)
        {
            Libro temp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = temp;
        }

        public bool Vacio() 
        {
            if (_tamano == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class MinHeap // Clase que representa un heap minimo para los libros con menos copias disponibles
    {
        private Libro[] _heap;
        private int _tamano;

        public MinHeap(int capacidad = 10)
        {
            _heap = new Libro[capacidad];
            _tamano = 0;
        }

        public void Insertar(Libro libro)
        {
            if (_tamano == _heap.Length) 
            {
                Redimensionar();
            }
            
            _heap[_tamano] = libro;
            HeapifyUp(_tamano);
            _tamano++;
        }

        public Libro ExtraerMinimo() // Metodo para extraer el libro con menos copias disponibles
        {
            if (_tamano == 0) // Si el heap esta vacio
            {
                return null;
            }
            else
            {
                Libro min = _heap[0]; 
                
                _tamano--;
                _heap[0] = _heap[_tamano]; 
                
                HeapifyDown(0); 
                return min;
            }
        }

        private void HeapifyUp(int i)
        {
            while (true)
            {
                if (i > 0)
                {
                    int padre = (i - 1) / 2;
                    
                    if (_heap[i].copiasdisponibles < _heap[padre].copiasdisponibles)
                    {
                        Intercambiar(i, padre);
                        i = padre;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void HeapifyDown(int i)
        {
            while (true)
            {
                int menor = i;
                int izq = 2 * i + 1;
                int der = 2 * i + 2;
                
                if (izq < _tamano)
                {
                    if (_heap[izq].copiasdisponibles < _heap[menor].copiasdisponibles)
                    {
                        menor = izq;
                    }
                }

                if (der < _tamano)
                {
                    if (_heap[der].copiasdisponibles < _heap[menor].copiasdisponibles)
                    {
                        menor = der;
                    }
                }

                if (menor == i) 
                {
                    break;
                }
                else
                {
                    Intercambiar(i, menor);
                    i = menor;
                }
            }
        }

        private void Redimensionar() // Metodo para redimensionar el heap cuando se llena
        {
            Libro[] nuevo = new Libro[_heap.Length * 2]; // Duplicamos la capacidad del heap
            for (int i = 0; i < _tamano; i++) 
            {
                nuevo[i] = _heap[i];
            }
            _heap = nuevo;
        }

        private void Intercambiar(int i, int j) // Metodo para intercambiar dos elementos en el heap
        {
            Libro temp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = temp;
        }

        public bool Vacio()  // Verficia si el heap esta vacio
        {
            if (_tamano == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class NodoBMas // Clase que representa un nodo en el arbol B+
    {
        public bool EsHoja { get; set; } 
        public int Contador { get; set; } // Numero de claves en el nodo    
        public string[] Claves { get; set; } // Claves de los libros en el nodo
        public Libro[] Valores { get; set; } // Solo se usa si es hoja
        public NodoBMas[] Hijos { get; set; } 
        public NodoBMas Padre { get; set; } 
        public NodoBMas Siguiente { get; set; } 

        public NodoBMas(int orden, bool esHoja) // Constructor del nodo B+
        {
            EsHoja = esHoja;
            Contador = 0;
            Claves = new string[orden];  
            
            if (esHoja) 
            {
                Valores = new Libro[orden]; 
            }
            else 
            {
                Hijos = new NodoBMas[orden + 1]; 
            }
        }
    }

    public class ArbolBMas // Clase que representa el arbol B+
    {
        private NodoBMas _raiz; 
        private readonly int _orden; 
        private NodoBMas _primeraHoja; 

        public ArbolBMas(int orden = 4)
        {
            _orden = orden;
            _raiz = new NodoBMas(orden, true); 
            _primeraHoja = _raiz;
        }

        public void Insertar(string clave, Libro libro) // Recibe como parametro la clave y el libro a insertar
        {
            NodoBMas hoja = EncontrarHoja(_raiz, clave); 
            InsertarEnNodo(hoja, clave, libro, null); 
        }

        public Libro Buscar(string clave) // Metodo para buscar un libro en el arbol B+ con la clave proporcionada (codigo del libro)
        {
            NodoBMas hoja = EncontrarHoja(_raiz, clave);
            for (int i = 0; i < hoja.Contador; i++)
            {
                if (hoja.Claves[i] == clave) 
                {
                    return hoja.Valores[i]; 
                }
            }
            return null; 
        }

        public Libro[] ObtenerTodos() // Metodo para obtener todos los libros en el arbol B+
        {
            int total = 0; 
            NodoBMas actual = _primeraHoja; 
            
            while (actual != null)
            {
                total += actual.Contador; 
                actual = actual.Siguiente; 
            }

            Libro[] resultado = new Libro[total]; 
            int idx = 0; 
            actual = _primeraHoja;
            
            while (actual != null)
            {
                for (int i = 0; i < actual.Contador; i++)
                {
                    resultado[idx++] = actual.Valores[i];
                }
                actual = actual.Siguiente;
            }
            return resultado;
        }

        private NodoBMas EncontrarHoja(NodoBMas nodo, string clave)
        {
            if (nodo.EsHoja) 
            {
                return nodo;
            }
            
            for (int i = 0; i < nodo.Contador; i++)
            {
                if (string.Compare(clave, nodo.Claves[i], StringComparison.OrdinalIgnoreCase) < 0)  // Si la clave es menor que la clave en el nodo, vamos al hijo correspondiente
                {
                    return EncontrarHoja(nodo.Hijos[i], clave);
                }
            }
            return EncontrarHoja(nodo.Hijos[nodo.Contador], clave);
        }

        private void InsertarEnNodo(NodoBMas nodo, string clave, Libro libro, NodoBMas hijoDerecho)
        {
            int i = nodo.Contador - 1;
            
            while (i >= 0 && string.Compare(clave, nodo.Claves[i], StringComparison.OrdinalIgnoreCase) < 0)
            {
                nodo.Claves[i + 1] = nodo.Claves[i];
                if (nodo.EsHoja) 
                {
                    nodo.Valores[i + 1] = nodo.Valores[i];
                }
                else 
                {
                    nodo.Hijos[i + 2] = nodo.Hijos[i + 1];
                }
                i--;
            }
            
            nodo.Claves[i + 1] = clave;
            if (nodo.EsHoja) 
            {
                nodo.Valores[i + 1] = libro;
            }
            else 
            {
                nodo.Hijos[i + 2] = hijoDerecho;
            }
            
            nodo.Contador++;

            if (nodo.Contador == _orden) 
            {
                Dividir(nodo); 
            }
        }

        private void Dividir(NodoBMas nodo)
        {
            int mitad = _orden / 2;
            NodoBMas nuevoNodo = new NodoBMas(_orden, nodo.EsHoja);
            
            int j = 0;
            int inicioCopia;
            if (nodo.EsHoja)
            {
                inicioCopia = mitad;
            }
            else
            {
                inicioCopia = mitad + 1;
            }

            for (int i = inicioCopia; i < _orden; i++)
            {
                nuevoNodo.Claves[j] = nodo.Claves[i];
                if (nodo.EsHoja) 
                {
                    nuevoNodo.Valores[j] = nodo.Valores[i];
                }
                else
                {
                    nuevoNodo.Hijos[j] = nodo.Hijos[i];
                    nuevoNodo.Hijos[j].Padre = nuevoNodo;
                }
                j++;
            }

            if (!nodo.EsHoja)
            {
                nuevoNodo.Hijos[j] = nodo.Hijos[_orden];
                nuevoNodo.Hijos[j].Padre = nuevoNodo;
            }

            nuevoNodo.Contador = _orden - inicioCopia;
            nodo.Contador = mitad;

            if (nodo.EsHoja)
            {
                nuevoNodo.Siguiente = nodo.Siguiente;
                nodo.Siguiente = nuevoNodo;
            }

            string claveGuia = nodo.Claves[mitad];

            if (nodo.Padre == null)
            {
                NodoBMas nuevaRaiz = new NodoBMas(_orden, false);
                nuevaRaiz.Claves[0] = claveGuia;
                nuevaRaiz.Hijos[0] = nodo;
                nuevaRaiz.Hijos[1] = nuevoNodo;
                nuevaRaiz.Contador = 1;
                nodo.Padre = nuevaRaiz;
                nuevoNodo.Padre = nuevaRaiz;
                _raiz = nuevaRaiz;
            }
            else 
            {
                nuevoNodo.Padre = nodo.Padre;
                InsertarEnNodo(nodo.Padre, claveGuia, null, nuevoNodo);
            }
        }
    }


    public class Biblioteca
    {
        private ArbolBMas _indiceLibros;
        private const string ArchivoTxt = "libros.txt";

        public Biblioteca()
        {
            _indiceLibros = new ArbolBMas(4); 
        }

        public void RegistrarLibro(string codigo, string titulo, string autor, string categoria, int copias, bool guardarEnDisco = true)
        {
            if (_indiceLibros.Buscar(codigo) != null)
            {
                Console.WriteLine("Ingrese un codigo unico, ya existe un libro con ese codigo");
                return;
            }
            else
            {
                Libro nuevo = new Libro(codigo, titulo, autor, categoria, copias);
                _indiceLibros.Insertar(codigo, nuevo);
                
                if (guardarEnDisco)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(ArchivoTxt, true)) 
                        {
                            sw.WriteLine($"{codigo}|{titulo}|{autor}|{categoria}|{copias}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("No se pudo escribir en el archivo físico");
                    }
                }

                Console.WriteLine("Libro registrado con éxito.");
            }
        }

        public void BuscarLibro(string codigo)
        {
            Libro libro = _indiceLibros.Buscar(codigo);
            if (libro != null)
            {
                Console.WriteLine($"Encontrado: {libro.Titulo} | Autor: {libro.Autor} | copias: {libro.copiasdisponibles} | prestamos totales: {libro.VecesPrestado}");
            }
            else
            {
                Console.WriteLine("Libro no encontrado.");
            }
        }

        public void RealizarPrestamo(string codigo)
        {
            Libro libro = _indiceLibros.Buscar(codigo);
            if (libro == null)
            {
                Console.WriteLine("Ingrese un libro existente");
                return;
            }
            else
            {
                if (libro.Prestar()) 
                {
                    Console.WriteLine($"prestamo exitoso. copias restantes: {libro.copiasdisponibles}");
                }
                else 
                {
                    Console.WriteLine("No hay mas copias disponibles de este libro");
                }
            }
        }

        public void RegistrarDevolucion(string codigo)
        {
            Libro libro = _indiceLibros.Buscar(codigo);
            if (libro != null)
            {
                libro.Devolver();
                Console.WriteLine($"devolucion registrada. Stock actual: {libro.copiasdisponibles}");
            }
            else
            {
                Console.WriteLine("Ingrese un libro existente");
            }
        }

        public void MostrarCatalogoOrdenado()
        {
            Libro[] todos = _indiceLibros.ObtenerTodos();
            if (todos.Length == 0)
            {
                Console.WriteLine("No hay libros registrados");
                return;
            }
            else
            {
                QuickSortTitulos(todos, 0, todos.Length - 1); 
                
                Console.WriteLine("\n--- Catalogo Ordenado por titulo ---");
                foreach (var l in todos)
                {
                    Console.WriteLine($"- {l.Titulo} (codigo: {l.Codigo}) | Autor: {l.Autor} | Stock: {l.copiasdisponibles}");
                }
            }
        }

        public void MostrarMasPrestados(int cantidad = 5)
        {
            Libro[] todos = _indiceLibros.ObtenerTodos();
            
            int capacidadInicialMax;
            if (todos.Length > 0)
            {
                capacidadInicialMax = todos.Length;
            }
            else
            {
                capacidadInicialMax = 10;
            }

            MaxHeap heap = new MaxHeap(capacidadInicialMax);
            
            foreach (var l in todos)
            {
                if (l.VecesPrestado > 0) 
                {
                    heap.Insertar(l);
                }
            }

            Console.WriteLine("\n--- Libros mas Prestados ---");
            int c = 0;
            
            while (true)
            {
                if (heap.Vacio())
                {
                    break;
                }
                else
                {
                    if (c >= cantidad)
                    {
                        break;
                    }
                    else
                    {
                        Libro extraido = heap.ExtraerMaximo();
                        c++;
                        Console.WriteLine($"{c}. {extraido.Titulo} - {extraido.VecesPrestado} prestamos");
                    }
                }
            }

            if (c == 0) 
            {
                Console.WriteLine("No hay prestamos registrados");
            }
        }

        public void MostrarInventarioCritico(int cantidad = 5)
        {
            Libro[] todos = _indiceLibros.ObtenerTodos();
            
            int capacidadInicialMin;
            if (todos.Length > 0)
            {
                capacidadInicialMin = todos.Length;
            }
            else
            {
                capacidadInicialMin = 10;
            }

            MinHeap heap = new MinHeap(capacidadInicialMin);
            
            foreach (var l in todos)
            {
                heap.Insertar(l);
            }

            Console.WriteLine("\n--- Inventario critico ---");
            int c = 0;
            
            while (true)
            {
                if (heap.Vacio())
                {
                    break;
                }
                else
                {
                    if (c >= cantidad)
                    {
                        break;
                    }
                    else
                    {
                        Libro extraido = heap.ExtraerMinimo();
                        c++;
                        Console.WriteLine($"{c}. {extraido.Titulo} - Quedan {extraido.copiasdisponibles} copias");
                    }
                }
            }
        }

        public void CargarDesdeArchivo(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine($"No se encontró el archivo '{rutaArchivo}'");
                return;
            }

            try
            {
                using (StreamReader lector = new StreamReader(rutaArchivo))
                {
                    string linea;
                    while ((linea = lector.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linea)) continue;

                        string[] datos = linea.Split('|'); 
                        
                        if (datos.Length == 5)
                        {
                            string codigo = datos[0].Trim();
                            string titulo = datos[1].Trim();
                            string autor = datos[2].Trim();
                            string categoria = datos[3].Trim();
                            
                            if (int.TryParse(datos[4].Trim(), out int copias))
                            {
                                if (_indiceLibros.Buscar(codigo) == null)
                                {
                                    RegistrarLibro(codigo, titulo, autor, categoria, copias, false);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hubo un error al leer el archivo");
            }
        }

        private void QuickSortTitulos(Libro[] arr, int izq, int der)
        {
            if (izq < der)
            {
                int pivot = Particionar(arr, izq, der);
                QuickSortTitulos(arr, izq, pivot - 1);
                QuickSortTitulos(arr, pivot + 1, der);
            }
        }

        private int Particionar(Libro[] arr, int izq, int der)
        {
            string pivotStr = arr[der].Titulo;
            int i = izq - 1;
            for (int j = izq; j < der; j++)
            {
                if (string.Compare(arr[j].Titulo, pivotStr, StringComparison.OrdinalIgnoreCase) <= 0)
                {
                    i++;
                    Libro temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            Libro t = arr[i + 1];
            arr[i + 1] = arr[der];
            arr[der] = t;
            return i + 1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca();
            bool salir = false;
            
            biblioteca.CargarDesdeArchivo("libros.txt");

            while (!salir)
            {
                Console.WriteLine("\n--- Sistema de Biblioteca ---");
                Console.WriteLine("1. Registrar nuevo libro");
                Console.WriteLine("2. Buscar libro por codigo");
                Console.WriteLine("3. Registrar prestamo");
                Console.WriteLine("4. Registrar devolucion");
                Console.WriteLine("5. Mostrar catalogo ordenado");
                Console.WriteLine("6. Libros mas prestados");
                Console.WriteLine("7. Inventario Critico");
                Console.WriteLine("8. Salir");
                Console.Write("Ingrese una opcion: ");
                
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("codigo: "); 
                        string cod = Console.ReadLine();
                        Console.Write("titulo: "); 
                        string tit = Console.ReadLine();
                        Console.Write("Autor: "); 
                        string aut = Console.ReadLine();
                        Console.Write("categoria: "); 
                        string cat = Console.ReadLine();
                        Console.Write("copias disponibles: "); 
                        
                        if(int.TryParse(Console.ReadLine(), out int copias))
                        {
                            biblioteca.RegistrarLibro(cod, tit, aut, cat, copias, true);
                        }
                        else
                        {
                            Console.WriteLine("Ingrese un valor valido");
                        }
                        break;
                        
                    case "2":
                        Console.Write("Ingrese codigo del libro a buscar: ");
                        biblioteca.BuscarLibro(Console.ReadLine());
                        break;
                        
                    case "3":
                        Console.Write("Ingrese codigo del libro a prestar: ");
                        biblioteca.RealizarPrestamo(Console.ReadLine());
                        break;
                        
                    case "4":
                        Console.Write("Ingrese codigo del libro a devolver: ");
                        biblioteca.RegistrarDevolucion(Console.ReadLine());
                        break;
                        
                    case "5":
                        biblioteca.MostrarCatalogoOrdenado();
                        break;
                        
                    case "6":
                        biblioteca.MostrarMasPrestados();
                        break;
                        
                    case "7":
                        biblioteca.MostrarInventarioCritico();
                        break;
                        
                    case "8":
                        salir = true;
                        break;
                        
                    default:
                        Console.WriteLine("Ingrese una opcion valida");
                        break;
                }
            }
        }
    }
}