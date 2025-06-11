import { useState, useEffect } from 'react';
import { useFeedback } from '../../hooks/useFeedback';
import { useAutoResizeTextarea } from '../../hooks/useAutoResizeTextarea';
import { EMOJIS } from '../../utils/constants';
import { EmojiRating } from './EmojiRating';
import { ThankYouMessage } from './ThankYouMessage';
import { FeedbackList } from './FeedbackList';
import './FeedbackForm.css';

export const FeedbackForm = () => {
  const [name, setName] = useState('');
  const [feedback, setFeedback] = useState('');
  const [selectedEmoji, setSelectedEmoji] = useState<number | null>(null);
  const [isSubmitted, setIsSubmitted] = useState(false);
  const [isFormValid, setIsFormValid] = useState(false);
  const [submittedName, setSubmittedName] = useState('');

  const { feedbacks, saveFeedback } = useFeedback();
  const autoResizeTextarea = useAutoResizeTextarea();

  useEffect(() => {
    setIsFormValid(
      name.trim() !== '' && 
      feedback.trim() !== '' && 
      selectedEmoji !== null
    );
  }, [name, feedback, selectedEmoji]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!isFormValid) return;

    const newFeedback = {
      name,
      feedback,
      rating: selectedEmoji as number,
    };

    saveFeedback(newFeedback);
    setSubmittedName(name);
    setName('');
    setFeedback('');
    setSelectedEmoji(null);
    setIsSubmitted(true);
  };

  const handleReset = () => {
    setIsSubmitted(false);
  };

  if (isSubmitted) {
    return <ThankYouMessage name={submittedName} onReset={handleReset} />;
  }

  return (
    <div className="form-wrapper">
      <div className="feedback-container">
        <h2>Помогите нам сделать процесс бронирования лучше</h2>
        
        <EmojiRating 
          selectedEmoji={selectedEmoji} 
          onSelect={setSelectedEmoji} 
          emojis={EMOJIS} 
        />

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="Как вас зовут?"
              required
            />
          </div>
          
          <div className="form-group">
            <textarea
              value={feedback}
              onChange={(e) => {
                setFeedback(e.target.value);
                autoResizeTextarea(e.target);
              }}
              placeholder="Напишите, что понравилось, что было непонятно"
              required
            />
          </div>
          
          <button 
            type="submit" 
            className={`submit-btn ${isFormValid ? 'active' : ''}`}
            disabled={!isFormValid}
          >
            Отправить
          </button>
        </form>
      </div>

      <FeedbackList feedbacks={feedbacks} emojis={EMOJIS} />
    </div>
  );
};