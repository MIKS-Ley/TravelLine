import type { Feedback, EmojiOption } from '../../types/feedback';
import './FeedbackForm.css';

interface FeedbackListProps {
  feedbacks: Feedback[];
  emojis: EmojiOption[];
}

export const FeedbackList = ({ feedbacks, emojis }: FeedbackListProps) => {
  if (feedbacks.length === 0) return null;

  return (
    <div className="all-feedbacks">
      <h3>Предыдущие отзывы:</h3>
      {feedbacks
        .sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime())
        .map((fb) => (
          <div key={fb.id} className="feedback-item">
            <div className="user-info">
              <div className="avatar">
                {fb.name.charAt(0).toUpperCase()}
              </div>
              <div className="user-details">
                <h4>{fb.name}</h4>
                <div className="rating">
                  <span>{fb.rating}/5</span>
                  <span className="emoji-preview">
                    {emojis.find(e => e.id === fb.rating)?.icon}
                  </span>
                </div>
              </div>
            </div>
            <div className="feedback-content">
              <p className='comment'>{fb.feedback}</p>
            </div>
          </div>
        ))}
    </div>
  );
};