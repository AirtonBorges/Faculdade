# Endereços interessantes 

- 1
- 256
- 257
- 262144 (256x1024) (1 Página no caso 32bits)

## Pensando

6 - QuantidadeBitsPagina
2 - DeslocamentoPagina
2 - NumeroBitsSubPagina
101111 - Input

110000 (111111 << DeslocamentoPagina + NumeroBitsSubPagina)
001100  ((111111 << DeslocamentoPagina) >> QuantidadeBitsPagina)
000011 (~(111111 << 2))