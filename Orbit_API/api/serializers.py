from rest_framework import routers, serializers, viewsets
from .models import *


# Serializers define the API representation.
class CelestialBodySerializer(serializers.ModelSerializer):
    class Meta:
        model = CelestialBody
        fields = '__all__'


class StartParamsSerializer(serializers.ModelSerializer):
    class Meta:
        model = StartParams
        fields = '__all__'


class LaunchParamsSerializer(serializers.ModelSerializer):
    class Meta:
        model = LaunchParams
        fields = '__all__'


class ResultSerializer(serializers.ModelSerializer):
    class Meta:
        model = Result
        fields = '__all__'
