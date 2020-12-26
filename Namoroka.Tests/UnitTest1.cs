using System;
using NamorokaV2.NamorokaCore.Modules.Audio;
using Victoria;
using Xunit;

namespace Namoroka.Tests
{
    public class NamorokaAudio
    {
        private LavaNode _lavaNode;
        [Theory]
        [InlineData("-skip", true)]
        public bool TestAudioSkipped(string input, bool expectedOutput)
        {
            var audio = new AudioModule(_lavaNode);
        }
    }
}