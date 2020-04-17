from rest_framework import viewsets

from api.serializers import CelestialBodySerializer, LaunchParamsSerializer, StartParamsSerializer, ResultSerializer
from .models import *


# Create your views here.
class CelestialBodyViewSet(viewsets.ModelViewSet):
    serializer_class = CelestialBodySerializer
    queryset = CelestialBody.objects.all()


class StartParamsViewSet(viewsets.ModelViewSet):
    serializer_class = StartParamsSerializer
    queryset = StartParams.objects.all()


class LaunchParamsViewSet(viewsets.ModelViewSet):
    serializer_class = LaunchParamsSerializer
    queryset = LaunchParams.objects.all()


class ResultsViewSet(viewsets.ModelViewSet):
    serializer_class = ResultSerializer
    queryset = Result.objects.all()
