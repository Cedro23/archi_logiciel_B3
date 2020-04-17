from django.db import models


# Create your models here.

class CelestialBody(models.Model):
    # mass en T, radius en km
    name = models.TextField()
    mass = models.FloatField()
    radius = models.FloatField()

    def __str__(self):
        return self.name


class StartParams(models.Model):
    station_mass = models.FloatField()
    distance = models.FloatField()
    celestial_body = models.ForeignKey(CelestialBody, on_delete=models.CASCADE)


class LaunchParams(models.Model):
    speed = models.FloatField()
    angle = models.FloatField()


class Result(models.Model):
    start_params = models.ForeignKey(StartParams, related_name="start_params", on_delete=models.CASCADE)
    launch_params = models.ForeignKey(LaunchParams, related_name="launch_params", on_delete=models.CASCADE)
    iteration_nb = models.IntegerField()
    success = models.BooleanField()
    date_time = models.DateTimeField()
    sim_duration = models.BigIntegerField()
