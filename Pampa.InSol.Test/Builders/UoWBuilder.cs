using Moq;
using Pampa.InSol.Dal;
using Pampa.InSol.Dal.Soporte;
using Pampa.InSol.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Pampa.InSol.Test
{
    /// <summary>
    /// Builder para crear repositorios mockeados
    /// </summary>
    internal class UoWBuilder
    {
        private Mock<AppDBContext> mockContext;
        private Mock<UnitOfWork> uow;
        private Dictionary<Type, object> dbSetsConfigurados = new Dictionary<Type, object>();

        public UoWBuilder()
        {
            this.mockContext = new Mock<AppDBContext>();
            this.uow = new Mock<UnitOfWork>(this.mockContext.Object);
        }

        public UnitOfWork Build()
        {
            return this.uow.Object;
        }

        public Mock<UnitOfWork> BuildMock()
        {
            return this.uow;
        }

        public UoWBuilder WithData<T>(IQueryable<T> data) where T : BaseEntity
        {
            Mock<DbSet<T>> mockSet = this.GetMockedSet(data);

            this.mockContext.Setup(c => c.Set<T>()).Returns(() => mockSet.Object);
            var repoRol = new Mock<Repository<T>>(this.mockContext.Object) { CallBase = true };
            this.uow.Setup(c => c.Repository<T>()).Returns(() => repoRol.Object);
            return this;
        }

        public UoWBuilder WithPersistibleData<T>(IList<T> data) where T : BaseEntity
        {
            Mock<DbSet<T>> mockSet = this.GetMockedSet(data.AsQueryable());
            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback((T obj) => data.Add(obj));

            this.mockContext.Setup(c => c.Set<T>()).Returns(() => mockSet.Object);
            var repoRol = new Mock<Repository<T>>(this.mockContext.Object) { CallBase = true };
            this.uow.Setup(c => c.Repository<T>()).Returns(() => repoRol.Object);
            return this;
        }

        public UoWBuilder WithStoredProcedureDinamicoData<T>(string storedProcedureName, List<T> resultados)
        {
            this.uow.Setup(x => x.InvocarStoreProcedureDinamico<T>(storedProcedureName, It.IsAny<Dictionary<string, object>>())).Returns(resultados);
            return this;
        }

        public UoWBuilder WithFunctionData<T>(string functionName, T resultado)
        {
            this.uow.Setup(x => x.InvocarFuncion<T>(functionName, It.IsAny<Dictionary<string, object>>())).Returns(resultado);
            return this;
        }

        public UoWBuilder WithTransaction()
        {
            DbContextTransaction mock = new Mock<DbContextTransaction>().Object;
            this.uow.Setup(x => x.IniciarTransacion()).Returns(mock);
            return this;
        }

        public Mock<DbSet<T>> GetDbSetsConfigurado<T>() where T : BaseEntity
        {
            return (Mock<DbSet<T>>)this.dbSetsConfigurados[typeof(T)];
        }

        private Mock<DbSet<T>> GetMockedSet<T>(IQueryable<T> data) where T : BaseEntity
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            this.dbSetsConfigurados.Add(typeof(T), mockSet);
            return mockSet;
        }
    }
}
