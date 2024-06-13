// Copyright (C) 2016 freeyouth
//
// Author: freeyouth <343800563@qq.com>
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
//Code:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEngine;
namespace XEngine.UI
{

    public class XUISpec
    {
        static public readonly XUISpec None = new XUISpec();
        static public readonly XUISpec DisVisible = new XUISpec();
        static public readonly XUISpec Visible = new XUISpec();
        static public readonly XUISpec Disable = new XUISpec();
		static public readonly XUISpec Enable = new XUISpec();
		static public readonly XUISpec NormalColor = new XUISpec();
		static public readonly XUISpec Gray = new XUISpec();
        static public readonly XUISpec GrayMaskColor = new XUISpec();//带遮罩变灰
        static public readonly XUISpec NormalMaskColor = new XUISpec();//带遮罩变灰
    }


}
