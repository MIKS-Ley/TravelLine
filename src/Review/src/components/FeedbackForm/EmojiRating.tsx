// EmojiRating.tsx
import type { EmojiOption } from '../../types/feedback';
import './FeedbackForm.css';

interface EmojiRatingProps {
  selectedEmoji: number | null;
  onSelect: (id: number) => void;
  emojis: EmojiOption[];
}

export const EmojiRating = ({ selectedEmoji, onSelect, emojis }: EmojiRatingProps) => (
  <div className="emoji-container">
    {emojis.map((emoji) => (
      <div
        key={emoji.id}
        className={`emoji-option ${selectedEmoji === emoji.id ? 'selected' : ''}`}
        onClick={() => onSelect(emoji.id)}
      >
        <span className="emoji">{emoji.icon}</span>
        <input
          type="radio"
          name="rating"
          value={emoji.id}
          checked={selectedEmoji === emoji.id}
          onChange={() => {}}
          className="hidden-input"
        />
      </div>
    ))}
  </div>
);