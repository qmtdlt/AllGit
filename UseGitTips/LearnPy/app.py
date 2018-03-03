import matplotlib.pyplot as plt
import math
import numpy as np

a = list(np.arange(0, 20, 0.1))

s1 = list(map(math.sin, a))

s2 = list(map(lambda v: math.sin(v-math.pi), a))

plt.plot(a, s1, label="SIN(X)")
plt.plot(a, s2, label="SIN(X-PI)")
plt.legend()
plt.show()
