- Locks, são responsáveis por bloquear recursos para que ele seja acessado por uma thread
- É feita uma operação para obter um bloqueio, adquirir()
- E outra para liberar o acesso, o liberar()

É criada uma variável global, para armazenar o lock da seção crítica

é chamado um método para obter o lock 

O Mutex cria uma fila especial para esperar o lock, de bloqueados

ao modificar a seção crítica, é liberada a seção crítica, usando o liberar()

