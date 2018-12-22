﻿using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Esp.Models
{
    public class Comando : INotifyPropertyChanged
    {
        private string _id;

        [PrimaryKey, AutoIncrement]
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                NotifyPropertyChanged();
            }
        }

        private string _type;

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime d1;

        private DateTime d2;

        private string _ip;

        public string IP
        {
            get { return _ip; }
            set
            {
                _ip = value;
                NotifyPropertyChanged();
            }
        }

        private int _port;

        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                NotifyPropertyChanged();
            }
        }

        public Comando()
        {
        }

        public Comando(string send, string ip, int port, string type)
        {
            this.Send = send;
            this.IP = ip;
            this.Port = port;
            this.Type = type;
        }

        private string _responseTime;

        public string ResponseTime
        {
            get { return $"{_responseTime} ms"; }
            set
            {
                _responseTime = value;
                NotifyPropertyChanged();
            }
        }

        private string _send;

        public string Send
        {
            get { return _send; }
            set
            {
                _send = value;
                d1 = DateTime.Now;
                NotifyPropertyChanged();
            }
        }

        private string _receive = "No reply.";

        public string Receive
        {
            get { return _receive; }
            set
            {
                if (value.IndexOf('\0') == -1)
                {
                    _receive = value;
                }
                else
                {
                    _receive = value.Substring(0, value.IndexOf('\0'));
                }
                d2 = DateTime.Now;
                TimeSpan diff = d2 - d1;
                ResponseTime = Math.Round(diff.TotalMilliseconds, 1, MidpointRounding.ToEven).ToString();
                NotifyPropertyChanged();
            }
        }

        private DateTime _dateTime = DateTime.Now;

        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                NotifyPropertyChanged();
            }
        }

        public override string ToString()
        {
            return $"Send: {Send} \n Receive: {Receive}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}