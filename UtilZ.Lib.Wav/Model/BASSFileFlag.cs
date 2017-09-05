﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilZ.Lib.Wav.Model
{
    /// <summary>
    /// 文件标识
    /// </summary>
    [Flags]
    public enum BASSFileFlag
    {
        /// <summary>
        /// 
        /// </summary>
        BASS_SAMPLE_8BITS = 1,

        /// <summary>
        /// 
        /// </summary>
        BASS_SAMPLE_FLOAT = 256,

        /// <summary>
        /// 
        /// </summary>
        BASS_SAMPLE_MONO = 2,

        /// <summary>
        /// 
        /// </summary>
        BASS_SAMPLE_SOFTWARE = 16,

        /// <summary>
        /// 
        /// </summary>
        BASS_SAMPLE_3D = 8,

        /// <summary>
        /// 
        /// </summary>
        BASS_SAMPLE_LOOP = 4,

        /// <summary>
        /// 
        /// </summary>
        BASS_SAMPLE_FX = 128,

        /// <summary>
        /// 
        /// </summary>
        BASS_STREAM_PRESCAN = 0x20000,

        /// <summary>
        /// 
        /// </summary>
        BASS_STREAM_AUTOFREE = 0x40000,

        /// <summary>
        /// 
        /// </summary>
        BASS_STREAM_DECODE = 0x200000,

        /// <summary>
        /// 
        /// </summary>
        BASS_ASYNCFILE = 0x40000000,

        /// <summary>
        /// 
        /// </summary>
        BASS_UNICODE = -2147483648

        //BASS_AAC_FRAME960 = 0x1000,
        //BASS_AAC_STEREO = 0x400000,
        //BASS_AC3_DOWNMIX_2 = 0x200,
        //BASS_AC3_DOWNMIX_4 = 0x400,
        //BASS_AC3_DOWNMIX_DOLBY = 0x600,
        //BASS_AC3_DYNAMIC_RANGE = 0x800,
        //BASS_ASYNCFILE = 0x40000000,
        //BASS_CD_C2ERRORS = 0x800,
        //BASS_CD_SUBCHANNEL = 0x200,
        //BASS_CD_SUBCHANNEL_NOHW = 0x400,
        //BASS_DEFAULT = 0,
        //BASS_DSD_DOP = 0x400,
        //BASS_DSD_RAW = 0x200,
        //BASS_DSHOW_NOAUDIO_PROC = 0x80000,
        //BASS_DSHOW_STREAM_AUTODVD = 0x4000000,
        //BASS_DSHOW_STREAM_LOOP = 0x8000000,
        //BASS_DSHOW_STREAM_MIX = 0x1000000,
        //BASS_DSHOW_STREAM_VIDEOPROC = 0x20000,
        //BASS_FX_BPM_BKGRND = 1,
        //BASS_FX_BPM_MULT2 = 2,
        //BASS_FX_FREESOURCE = 0x10000,
        //BASS_FX_TEMPO_ALGO_CUBIC = 0x400,
        //BASS_FX_TEMPO_ALGO_LINEAR = 0x200,
        //BASS_FX_TEMPO_ALGO_SHANNON = 0x800,
        //BASS_MIDI_DECAYEND = 0x1000,
        //BASS_MIDI_DECAYSEEK = 0x4000,
        //BASS_MIDI_FONT_MMAP = 0x20000,
        //BASS_MIDI_FONT_NOFX = 0x80000,
        //BASS_MIDI_FONT_XGDRUMS = 0x40000,
        //BASS_MIDI_NOCROP = 0x8000,
        //BASS_MIDI_NOFX = 0x2000,
        //BASS_MIDI_NOSYSRESET = 0x800,
        //BASS_MIDI_NOTEOFF1 = 0x10000,
        //BASS_MIDI_PACK_16BIT = 2,
        //BASS_MIDI_PACK_NOHEAD = 1,
        //BASS_MIDI_SINCINTER = 0x800000,
        //BASS_MIXER_BUFFER = 0x2000,
        //BASS_MIXER_DOWNMIX = 0x400000,
        //BASS_MIXER_END = 0x10000,
        //BASS_MIXER_LIMIT = 0x4000,
        //BASS_MIXER_MATRIX = 0x10000,
        //BASS_MIXER_NONSTOP = 0x20000,
        //BASS_MIXER_NORAMPIN = 0x800000,
        //BASS_MIXER_PAUSE = 0x20000,
        //BASS_MIXER_POSEX = 0x2000,
        //BASS_MIXER_RESUME = 0x1000,
        //BASS_MUSIC_3D = 8,
        //BASS_MUSIC_AUTOFREE = 0x40000,
        //BASS_MUSIC_DECODE = 0x200000,
        //BASS_MUSIC_FLOAT = 0x100,
        //BASS_MUSIC_FT2MOD = 0x2000,
        //BASS_MUSIC_FT2PAN = 0x2000,
        //BASS_MUSIC_FX = 0x80,
        //BASS_MUSIC_LOOP = 4,
        //BASS_MUSIC_MONO = 2,
        //BASS_MUSIC_NONINTER = 0x10000,
        //BASS_MUSIC_NOSAMPLE = 0x100000,
        //BASS_MUSIC_POSRESET = 0x8000,
        //BASS_MUSIC_POSRESETEX = 0x400000,
        //BASS_MUSIC_PRESCAN = 0x20000,
        //BASS_MUSIC_PT1MOD = 0x4000,
        //BASS_MUSIC_RAMP = 0x200,
        //BASS_MUSIC_RAMPS = 0x400,
        //BASS_MUSIC_SINCINTER = 0x800000,
        //BASS_MUSIC_STOPBACK = 0x80000,
        //BASS_MUSIC_SURROUND = 0x800,
        //BASS_MUSIC_SURROUND2 = 0x1000,
        //BASS_RECORD_AGC = 0x4000,
        //BASS_RECORD_ECHOCANCEL = 0x2000,
        //BASS_RECORD_PAUSE = 0x8000,
        //BASS_SAMPLE_3D = 8,
        //BASS_SAMPLE_8BITS = 1,
        //BASS_SAMPLE_FLOAT = 0x100,
        //BASS_SAMPLE_FX = 0x80,
        //BASS_SAMPLE_LOOP = 4,
        //BASS_SAMPLE_MONO = 2,
        //BASS_SAMPLE_MUTEMAX = 0x20,
        //BASS_SAMPLE_OVER_DIST = 0x30000,
        //BASS_SAMPLE_OVER_POS = 0x20000,
        //BASS_SAMPLE_OVER_VOL = 0x10000,
        //BASS_SAMPLE_SOFTWARE = 0x10,
        //BASS_SAMPLE_VAM = 0x40,
        //BASS_SPEAKER_CENLFE = 0x3000000,
        //BASS_SPEAKER_CENTER = 0x13000000,
        //BASS_SPEAKER_FRONT = 0x1000000,
        //BASS_SPEAKER_FRONTLEFT = 0x11000000,
        //BASS_SPEAKER_FRONTRIGHT = 0x21000000,
        //BASS_SPEAKER_LEFT = 0x10000000,
        //BASS_SPEAKER_LFE = 0x23000000,
        //BASS_SPEAKER_PAIR1 = 0x1000000,
        //BASS_SPEAKER_PAIR10 = 0xa000000,
        //BASS_SPEAKER_PAIR11 = 0xb000000,
        //BASS_SPEAKER_PAIR12 = 0xc000000,
        //BASS_SPEAKER_PAIR13 = 0xd000000,
        //BASS_SPEAKER_PAIR14 = 0xe000000,
        //BASS_SPEAKER_PAIR15 = 0xf000000,
        //BASS_SPEAKER_PAIR2 = 0x2000000,
        //BASS_SPEAKER_PAIR3 = 0x3000000,
        //BASS_SPEAKER_PAIR4 = 0x4000000,
        //BASS_SPEAKER_PAIR5 = 0x5000000,
        //BASS_SPEAKER_PAIR6 = 0x6000000,
        //BASS_SPEAKER_PAIR7 = 0x7000000,
        //BASS_SPEAKER_PAIR8 = 0x8000000,
        //BASS_SPEAKER_PAIR9 = 0x9000000,
        //BASS_SPEAKER_REAR = 0x2000000,
        //BASS_SPEAKER_REAR2 = 0x4000000,
        //BASS_SPEAKER_REAR2LEFT = 0x14000000,
        //BASS_SPEAKER_REAR2RIGHT = 0x24000000,
        //BASS_SPEAKER_REARLEFT = 0x12000000,
        //BASS_SPEAKER_REARRIGHT = 0x22000000,
        //BASS_SPEAKER_RIGHT = 0x20000000,
        //BASS_SPLIT_SLAVE = 0x1000,
        //BASS_STREAM_AUTOFREE = 0x40000,
        //BASS_STREAM_BLOCK = 0x100000,
        //BASS_STREAM_DECODE = 0x200000,
        //BASS_STREAM_PRESCAN = 0x20000,
        //BASS_STREAM_RESTRATE = 0x80000,
        //BASS_STREAM_STATUS = 0x800000,
        //BASS_UNICODE = -2147483648,
        //BASS_WV_STEREO = 0x400000
    }
}
