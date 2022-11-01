using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using XamarinEnterpriseApp.Xamarin.Core.Repositories.Entities;
using SQLite;

namespace XamarinEnterpriseApp.Xamarin.Core.Repositories
{
    public interface ILocalDbContextService
    {
        int SaveEntity<T>(T entity) where T : BaseEntity, new();

        int DeleteEntity<T>(T entity) where T : BaseEntity, new();

        List<T> GetEntityList<T>() where T : BaseEntity, new();

        T GetEntity<T>(int id) where T : BaseEntity, new();

        List<T> FilterEntity<T>(Expression<Func<T, bool>> filter) where T : BaseEntity, new();

        #region Helper Methods

        List<EnvironmentSetting> GetUserEnvironments();

        EnvironmentSetting GetSelectedEnvironment();

        PersonalInfo GetPersonalInfo();

        List<UserLocalReport> GetUserLocalReports();

        List<FollowedReport> GetFollowedReports();
        
        #endregion
    }

    public class LocalDbContextService : ILocalDbContextService
    {
        private SQLiteConnection _connection;
        private ILocalRepository _repository;

        public LocalDbContextService()
        {
            _connection = GetConnection();

            _connection.CreateTable<EnvironmentSetting>();
            _connection.CreateTable<WorkloadSetting>();
            _connection.CreateTable<SearchBarHistory>();
            _connection.CreateTable<PersonalInfo>();
            _connection.CreateTable<UserLocalReport>();
            _connection.CreateTable<FollowedReport>();
            _repository = new BaseRepository(_connection);
        }

        private SQLiteConnection GetConnection()
        {
            string filename = DbConstants.DatabaseFilename;

            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            string dbFilePath = Path.Combine(libFolder, filename);

            var connection = new SQLiteConnection(dbFilePath, DbConstants.Flags);

            return connection;
        }

        public int SaveEntity<T>(T entity) where T : BaseEntity, new()
        {
            int result = 0;

            try
            {
                result = _repository.Save(entity);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("SaveEntity", ex);
            }

            return result;
        }

        public int DeleteEntity<T>(T entity) where T : BaseEntity, new()
        {
            int result = 0;

            try
            {
                result = _repository.Delete(entity);
            }
            catch (Exception ex)
            {
                LogHelper.LogException("DeleteEntity", ex);
            }

            return result;
        }

        public int DeleteAll<T>() where T : BaseEntity, new()
        {
            int result = 0;

            try
            {
                result = _repository.DeleteAll<T>();
            }
            catch (Exception ex)
            {
                LogHelper.LogException("DeleteAllEntities", ex);
            }

            return result;
        }

        public List<T> GetEntityList<T>() where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            try
            {
                result = _repository.GetAll<T>();
            }
            catch (Exception ex)
            {
                LogHelper.LogException("GetEntityList", ex);
            }

            return result;
        }

        public T GetEntity<T>(int id) where T : BaseEntity, new()
        {
            T entity = null;

            try
            {
                entity = _repository.Get<T>(id);

            }
            catch (Exception ex)
            {
                LogHelper.LogException("GetEntity", ex);
            }

            return entity;
        }

        public List<T> FilterEntity<T>(Expression<Func<T, bool>> filter) where T : BaseEntity, new()
        {
            List<T> result = new List<T>();

            try
            {
                result = _repository.Filter(filter);

            }
            catch (Exception ex)
            {
                LogHelper.LogException("FilterEntity", ex);
            }

            return result;
        }

        #region Helper Methods

        public List<EnvironmentSetting> GetUserEnvironments()
        {
            List<EnvironmentSetting> endPoints = GetEntityList<EnvironmentSetting>();

            return endPoints;
        }

        public EnvironmentSetting GetSelectedEnvironment()
        {
            var endPoints = GetUserEnvironments();

            EnvironmentSetting selectedEndpoint = endPoints.FirstOrDefault(x => x.IsSelected);

            return selectedEndpoint;
        }

        public PersonalInfo GetPersonalInfo()
        {
            List<PersonalInfo> personalInfos = GetEntityList<PersonalInfo>();

            PersonalInfo personalInfo = personalInfos.FirstOrDefault();

            return personalInfo;
        }

        public List<UserLocalReport> GetUserLocalReports()
        {
            List<UserLocalReport> userLocalReports = GetEntityList<UserLocalReport>();

            return userLocalReports;
        }

        public List<FollowedReport> GetFollowedReports()
        {
            List<FollowedReport> followedReport = GetEntityList<FollowedReport>();

            return followedReport;
        }
        
        #endregion
    }
}
