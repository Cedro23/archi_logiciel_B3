from django.urls import path, include
from rest_framework import routers

from api.views import CelestialBodyViewSet, LaunchParamsViewSet, StartParamsViewSet, ResultsViewSet

router = routers.DefaultRouter()
router.register(r'celestialbody', CelestialBodyViewSet)
router.register(r'launchparams', LaunchParamsViewSet)
router.register(r'startparams', StartParamsViewSet)
router.register(r'results', ResultsViewSet)

urlpatterns = [
    path('', include(router.urls)),
    path('api-auth/', include('rest_framework.urls'))
]

urlpatterns += router.urls
