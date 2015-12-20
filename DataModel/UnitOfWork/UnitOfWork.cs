#region Using Namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Entity.Validation;
using DataModel.GenericRepository;

#endregion

namespace DataModel.UnitOfWork
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Private member variables...

        private DBEntities _context = null;
        private GenericRepository<OUmember> _ouMemberRepository;
        private GenericRepository<User> _userRepository;
        #endregion

        public UnitOfWork()
        {
            _context = new DBEntities();
        }

        #region Public Repository Creation properties...

        /// <summary>
        /// Get/Set Property for product repository.
        /// </summary>
        public GenericRepository<OUmember> OUMemberRepository
        {
            get
            {
                if (this._ouMemberRepository == null)
                    this._ouMemberRepository = new GenericRepository<OUmember>(_context);
                return _ouMemberRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new GenericRepository<User>(_context);
                return _userRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for token repository.
        /// </summary>
        //public GenericRepository<Token> TokenRepository
        //{
        //    get
        //    {
        //        if (this._tokenRepository == null)
        //            this._tokenRepository = new GenericRepository<Token>(_context);
        //        return _tokenRepository;
        //    }
        //}
        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
