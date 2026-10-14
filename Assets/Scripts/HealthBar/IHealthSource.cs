using System;

public interface IHealthSource {
    event Action<float> HealthChanged;
}
