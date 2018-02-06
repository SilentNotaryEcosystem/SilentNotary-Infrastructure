﻿
namespace In.Legacy.Mapping
{
    public class MapperServiceFactory
    {
        private readonly IDiScope _diContainer;

        public MapperServiceFactory(IDiScope diContainer)
        {
            _diContainer = diContainer;
        }

        public TDest GetFrom<TDest, TDto>(TDto model, object mappingData = null)
        {
            var mapperService = _diContainer.Resolve<IMapperService<TDest, TDto>>();
            return mapperService.GetFrom(model, mappingData);
        }
    }
}
