using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;

namespace PX.Objects.Common
{
	public class ProcessingResult: ProcessingResultBase<ProcessingResultMessage>
	{
		public override void AddMessage(PXErrorLevel errorLevel, string message, params object[] args)
		{
			_messages.Add(new ProcessingResultMessage(errorLevel, PXMessages.LocalizeFormatNoPrefix(message, args)));
		}

		public override void AddMessage(PXErrorLevel errorLevel, string message)
		{
			_messages.Add(new ProcessingResultMessage(errorLevel, PXMessages.LocalizeNoPrefix(message)));
		}
	}

	public abstract class ProcessingResultBase<TMessage>
	where TMessage: ProcessingResultMessage
	{
		protected readonly List<TMessage> _messages;

		public IReadOnlyList<TMessage> Messages
		{
			get { return _messages; }
		}

		/// <summary>
		/// true, if result does not have error level messages
		/// </summary>
		public virtual bool IsSuccess
		{
			get { return _messages.All(message => message.ErrorLevel != PXErrorLevel.RowError
												&& message.ErrorLevel != PXErrorLevel.Error);
			}
		}

		public bool HasWarning
		{
			get
			{
				return _messages.Any(message => message.ErrorLevel == PXErrorLevel.RowWarning
				                                || message.ErrorLevel == PXErrorLevel.Warning);
			}
		}

		public bool HasWarningOrError
		{
			get { return HasWarning || !IsSuccess; }
		}

		public PXErrorLevel MaxErrorLevel
		{
			get { return _messages.Max(message => message.ErrorLevel); }
		}

		public static ProcessingResult Success
		{
			get
			{
				return new ProcessingResult();
			}
		}

		public ProcessingResultBase()
		{
			_messages = new List<TMessage>();
		}

		public void AddErrorMessage(string message, params object[] args)
		{
			AddMessage(PXErrorLevel.Error, message, args);
		}

		public void AddErrorMessage(string message)
		{
			AddMessage(PXErrorLevel.Error, message);
		}

		public abstract void AddMessage(PXErrorLevel errorLevel, string message, params object[] args);

		public abstract void AddMessage(PXErrorLevel errorLevel, string message);

		public void Aggregate(ProcessingResultBase<TMessage> processingResult)
		{
			_messages.AddRange(processingResult.Messages);
		}

		public void RaiseIfHasError()
		{
			if (!IsSuccess)
				throw new PXException(GetGeneralMessage());
		}

		public virtual string GetGeneralMessage() => GeneralMessage;

		public virtual string GeneralMessage => string.Join(Environment.NewLine, Messages.Select(message => message.ToString()).ToArray());	
	}

	public class ProcessingResultMessage
	{
		public string Text { get; set; }

		public PXErrorLevel ErrorLevel { get; set; }

		public ProcessingResultMessage(PXErrorLevel errorLevel, string text)
		{
			Text = text;
			ErrorLevel = errorLevel;
		}

		public override string ToString()
		{
			string errorLevelPrefix = null;

			if (ErrorLevel == PXErrorLevel.Error || ErrorLevel == PXErrorLevel.RowError)
			{
				errorLevelPrefix = Common.Messages.Error;
			}
			else if (ErrorLevel == PXErrorLevel.Warning || ErrorLevel == PXErrorLevel.RowWarning)
			{
				errorLevelPrefix = Common.Messages.Warning;
			}

			return errorLevelPrefix != null
				? string.Concat(errorLevelPrefix, ": ", Text)
				: Text;
		}
	}
}