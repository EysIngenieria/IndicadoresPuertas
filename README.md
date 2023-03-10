# Indicadores de Puertas
Repositorio donde se guardarán los indicadores para los sistemas de puertas
# Información detallada de los indicadores

## Indicador RANO
Indicador de tiempo de solución de averías ANIO

```math
RANO = \frac{TCN}{TAN}
```
Donde 

```math
{TAN} 
``` 
Total de tickets con nivel de falla igual ANIO, ya sean abiertos o cerrados del periodo

```math
{TCN} 
``` 
Total de ticket con nivel de falla igual ANIO, cerrados en un intervalo menor o igual a 24 Horas

#### Consideraciones

Para TCN con estado cerrado: el tiempo a evaluar es fecha_cierre - fecha_apertura
Para TCN con estado abierto el tiempo a evaluar es fecha_apertura - fecha_cierre

Indicador de tiempo de solución de averías ANIO contratista


```math
RANO_C = \frac{TCN_C}{TAN_C}
``` 

TAN_C: # total de tickets con nivel de falla igual ANIO y diagnostico_causa igual a "a cargo de contratista", ya sean abiertos o cerrados del periodo.
TCN_C: # total de tickets con nivel de falla igual ANIO y diagnostico_causa igual a "a cargo de contratista", cerrados en un intervalo menor o igual a 24 horas.
Consideraciones: 
Para TCN_C con estado cerrado: el tiempo a evaluar es fecha_cierre - fecha_apertura.
Para TCN_C con estado abierto:  el tiempo a evaluar es fecha_actual - fecha_apertura.

Indicador de tiempo de solución de averías ANIO No Contratista

```math
RANO_NC = \frac{TCN_NC}{TAN_NC}
``` 
TAN_NC: # total de tickets con nivel de falla igual ANIO y diagnostico_causa diferente a "a cargo de contratista", ya sean abiertos o cerrados del periodo.
TCN_NC: # total de tickets con nivel de falla igual ANIO y diagnostico_causa diferente a "a cargo de contratista", cerrados en un intervalo menor o igual a 24 horas.
Consideraciones: 
Para TCN_NC con estado cerrado: el tiempo a evaluar es fecha_cierre - fecha_apertura.
Para TCN_NC con estado abierto:  el tiempo a evaluar es fecha_actual - fecha_apertura.

Condiciones especiales:

1. Si un ticket esta abierto y aun esta dentro del tiempo no se debe incluir en el calculo, no debe contar para ninguno de los contadores..

2. Si el valor TAN es 0 el resultado debe ser 100%.

## Indicador RANIO

## Indicador IEPM

## Indicador ICPM
