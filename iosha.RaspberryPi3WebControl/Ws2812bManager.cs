using iosha.RaspberryPi3WebControl.Storage;
using iosha.RaspberryPi3WebControl.Storage.Entity;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iosha.RaspberryPi3WebControl
{
    public class Ws2812bManager : IWs2812bManager
    {
        private readonly RaspberryPi3WebControlDbContext _dbContext;
        private readonly IWs2812bAdapter _ws2812BAdapter;

        private CancellationTokenSource _automodeCancelTokenSource;
        private CancellationToken _automodeTokenCancel;
        public bool IsAutomode { get; private set; }


        private LedSchema _staticSchema;

        public Ws2812bManager(IWs2812bAdapter ws2812BAdapter, RaspberryPi3WebControlDbContext dbContext)
        {
            _dbContext = dbContext;
            _ws2812BAdapter = ws2812BAdapter;
            InitSchema();
        }

        private void InitSchema()
        {
            var _staticSchema = _dbContext.LedSchemas.FirstOrDefault();
            if (_staticSchema == null)
                _staticSchema = new LedSchema() { Color = Color.White };

            LoadStaticSchema();
        }

        private void LoadStaticSchema()
        {
            SetAllPixels(_staticSchema.Color);           
        }


        public void SetAutomode(bool flag)
        {
            if (flag && !IsAutomode)
                StartAutoMode();

            if (!flag && IsAutomode)
                StopAutomode();
        }

        private void StartAutoMode()
        {
            IsAutomode = true;
            _automodeCancelTokenSource = new CancellationTokenSource();
            _automodeTokenCancel = _automodeCancelTokenSource.Token;

            Task automodeTask = new Task(() =>
            {
                while (true)
                {
                    _ws2812BAdapter.Clear();

                    for (int j = 0; j < _ws2812BAdapter.PixelCount; j++)
                    {
                        _ws2812BAdapter.SetPixel(j, Color.Green);
                        _ws2812BAdapter.Update();
                        System.Threading.Thread.Sleep(50);
                        if (_automodeTokenCancel.IsCancellationRequested)
                            return;
                        _ws2812BAdapter.SetPixel(j, Color.Green);
                        _ws2812BAdapter.Update();
                        System.Threading.Thread.Sleep(50);
                        if (_automodeTokenCancel.IsCancellationRequested)
                            return;
                    }
                }
            });
            automodeTask.Start();
        }

        private void StopAutomode()
        {
            if (_automodeCancelTokenSource == null || _automodeCancelTokenSource.IsCancellationRequested)
                return;
            _automodeCancelTokenSource.Cancel();           
            IsAutomode = false;
            Clear();
        }


        public void SetAllPixels(Color color)
        {
            SetAutomode(false);
            _ws2812BAdapter.SetAllPixels(color);
            _ws2812BAdapter.Update();
        }

        public void SetPixel(int position, Color color)
        {
            SetAutomode(false);
            _ws2812BAdapter.SetPixel(position, color);
            _ws2812BAdapter.Update();
        }


        public void Clear()
        {
            _ws2812BAdapter.Clear();
        }

        public void SaveSchema()
        {
            if (_staticSchema.Id == Guid.Empty)
                _dbContext.LedSchemas.Add(_staticSchema);
            else
                _dbContext.Add(_staticSchema);

            _dbContext.SaveChanges();
        }

        //public void SaveAsNewSchema(string Name,  )
        //{
        //    if (_staticSchema.Id == Guid.Empty)
        //        _dbContext.LedSchemas.Add(_staticSchema);
        //    else
        //        _dbContext.Add(_staticSchema);

        //    _dbContext.SaveChanges();
        //}
    }
}
